using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;
using OfBugsAndDevs.Services.Interfaces;

namespace OfBugsAndDevs.Services
{
    public class BlogPostAdminService : IBlogPostAdminService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public BlogPostAdminService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private async Task<TResult> ExecuteOnContext<TResult>(Func<ApplicationDbContext, Task<TResult>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            return await query.Invoke(context);

        }


        public async Task<PagedResult<BlogPost>> GetBlogPostsAsync(int startIndex, int pageSize)
        {
            return await ExecuteOnContext(async context =>
            {
                var query = context.BlogPosts
                                   .AsNoTracking();
                                   
                var count = await query.CountAsync();

                var records = await query.Include(b => b.Category)
                                         .OrderByDescending(b => b.BlogPostID)
                                         .Skip(startIndex)
                                         .Take(pageSize)
                                         .ToArrayAsync();


                return  new PagedResult<BlogPost>(records, count);
            });
        }

        public async Task<BlogPost?> GetBlogPostByIdAsync(int id) =>
            await ExecuteOnContext(async context =>
                 await context.BlogPosts
                              .AsNoTracking()
                              .Include(b => b.Category)
                              .FirstOrDefaultAsync(b => b.BlogPostID == id)
             );
           

        


        public Task<BlogPost> SaveBlogPostAsync(BlogPost blogPost, string userID)
        {
            return ExecuteOnContext(async context =>
            {
                if(blogPost.BlogPostID == 0)
                {
                    //new blog post
                    var isDupTitle = await context.BlogPosts
                                                .AsNoTracking()
                                                .AnyAsync(b => b.Title == blogPost.Title);
                    if (isDupTitle)
                    {
                        throw new InvalidOperationException($"Blog post with title {blogPost.Title} already exists");
                    }

                    blogPost.Slug = await GenerateSlug(blogPost);

                    blogPost.Created = DateTime.UtcNow;
                    blogPost.UserID = userID;

                    if (blogPost.IsPublished)
                    {
                        blogPost.Published = DateTime.UtcNow;
                    }
                    await context.BlogPosts.AddAsync(blogPost);
                }
                else
                {
                    //existing blog post
                    var isDupTitle = await context.BlogPosts
                                                .AsNoTracking()
                                                .AnyAsync(b => b.Title == blogPost.Title && b.BlogPostID != blogPost.BlogPostID);
                    if (isDupTitle)
                    {
                        throw new InvalidOperationException($"Blog post with title {blogPost.Title} already exists");
                    }

                    var dbBlog = await context.BlogPosts.FindAsync(blogPost.BlogPostID);
                    dbBlog.Title = blogPost.Title;
                    dbBlog.Image = blogPost.Image;
                    dbBlog.Introduction = blogPost.Introduction;
                    dbBlog.Content = blogPost.Content;
                    dbBlog.CategoryID = blogPost.CategoryID;
                    
                    dbBlog.IsFeatured = blogPost.IsFeatured;
                    dbBlog.UserID = blogPost.UserID;
                    dbBlog.IsPublished = blogPost.IsPublished;

                    if (blogPost.IsPublished)
                    {
                        if (!dbBlog.IsPublished)
                        {
                            blogPost.Published = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        blogPost.Published = null;
                    }
                }

                await context.AddRangeAsync();
                return blogPost;
            });
        }

        private async Task<string> GenerateSlug(BlogPost blogPost)
        {
            return await ExecuteOnContext(async context =>
            {
                string originalSlug = blogPost.Title.ToSlug();
                int count = 1;
                string slug = originalSlug;
                while(await context.BlogPosts.AsNoTracking().AnyAsync(b => b.Slug == slug))
                {
                    slug = $"{originalSlug}-{ count++} ";
                }
                return slug;
            });
        }

    }
}

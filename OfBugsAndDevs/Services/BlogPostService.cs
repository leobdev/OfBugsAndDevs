using Azure.Core;
using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;
using OfBugsAndDevs.Services.Interfaces;

namespace OfBugsAndDevs.Services
{
    public class BlogPostService : IBlogPostService
    {
        IDbContextFactory<ApplicationDbContext> _contextFactory;
        public BlogPostService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private async Task<TResult> QueryOnContextAsync<TResult>(Func<ApplicationDbContext, Task<TResult>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            return await query(context);
        }

        public async Task<DetailedPageModel> GetBlogPostBySlugAsync(string slug)
        {
            return await QueryOnContextAsync(async context =>
            {
                var blogPost = await context.BlogPosts
                                            .AsNoTracking()
                                            .Include(b => b.Category)
                                            .Include(b => b.User)
                                            .FirstOrDefaultAsync(b => b.Slug == slug && b.IsPublished);

                if (blogPost is null)
                    return DetailedPageModel.Empty();

                var relatedPosts = await context.BlogPosts
                                          .AsNoTracking()
                                          .Include(b => b.Category)
                                          .Include(b => b.User)
                                          .Where(b => b.CategoryID == blogPost.CategoryID && b.IsPublished)
                                          .OrderBy(_ => Guid.NewGuid())
                                          .Take(4)
                                          .ToArrayAsync();

                return new DetailedPageModel(blogPost, relatedPosts);

            });
        }

        public async Task<BlogPost[]> GetFeaturedBlogPostsAsync(int count, int categoryID = 0)
        {
            return await QueryOnContextAsync(async context =>
            {
                var query = context.BlogPosts.AsNoTracking()
                                             .Include(p => p.Category)
                                             .Include(p => p.User)
                                             .Where(b => b.IsPublished);
                if (categoryID > 0)
                {
                    query = query.Where(b => b.CategoryID == categoryID);
                }

                var records = await query.Where(b => b.IsFeatured)
                                         .OrderBy(_ => Guid.NewGuid())
                                         .Take(count)
                                         .ToArrayAsync();

                if(count > records.Length)
                {
                    var additionalRecords = await query.Where(b => !b.IsFeatured)
                                         .OrderBy(_ => Guid.NewGuid())
                                         .Take(count - records.Length)
                                         .ToArrayAsync();
                    records = [.. records, .. additionalRecords];
                }

                return records;



            });
        }

        public async Task<BlogPost[]> GetPopularBlogPostsAsync(int count, int categoryID = 0)
        {
            return await QueryOnContextAsync(async context =>
            {
                var query = context.BlogPosts.AsNoTracking()
                                             .Include(p => p.Category)
                                             .Include(p => p.User)
                                             .Where(b => b.IsPublished);
                if (categoryID > 0)
                {
                    query = query.Where(b => b.CategoryID == categoryID);
                }

                return await query.OrderByDescending(b => b.ViewCount)
                            .Take(count)
                            .ToArrayAsync();
            });
        }

        public async Task<BlogPost[]> GetRecentBlogPostsAsync(int count, int categoryID = 0) => 
            await GetPostsAsync(0, count, categoryID);

        public async Task<BlogPost[]> GetBlogPostsAsync(int pageIndex, int pageSize, int categoryID = 0) => 
            await GetPostsAsync(pageIndex * pageSize, pageSize, categoryID);
                
        

        private async Task<BlogPost[]> GetPostsAsync(int skip, int take, int categoryID)
        {
            return await QueryOnContextAsync(async context =>
            {
                var query = context.BlogPosts.AsNoTracking()
                                             .Include(p => p.Category)
                                             .Include(p => p.User)
                                             .Where(b => b.IsPublished);
                if (categoryID > 0)
                {
                    query = query.Where(b => b.CategoryID == categoryID);
                }

                return await query.OrderByDescending(b => b.Published)
                            .Skip(skip)
                            .Take(take)
                            .ToArrayAsync();
            });
        }

      
    }
}

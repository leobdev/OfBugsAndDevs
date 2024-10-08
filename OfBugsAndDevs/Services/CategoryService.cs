using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Services.Interfaces;

namespace OfBugsAndDevs.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public CategoryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private async Task<TResult> ExecuteOnContext<TResult>(Func<ApplicationDbContext, Task<TResult>> query)
        {
            using var context = _contextFactory.CreateDbContext();
            return await query.Invoke(context);

        }

        public async Task<Category[]> GetCategoriesAsync()
        {
            return await ExecuteOnContext(async context =>
            {
                return await context.Categories
                                    .AsNoTracking()
                                    .ToArrayAsync();

            });
        }

        public async Task<Category> SaveCategoryAsync(Category category)
        {
            return await ExecuteOnContext(async context =>
            {
                if (category.CategoryID == 0)
                {
                    //New category
                    if (await context.Categories
                                     .AsNoTracking()
                                     .AnyAsync(c => c.CategoryName == category.CategoryName))
                    {
                        throw new InvalidOperationException($"Category with the name {category.CategoryName} already exists");
                    }

                    category.Slug = category.CategoryName.ToSlug();
                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();

                }
                else
                {
                    //Existing category
                    if (await context.Categories
                                    .AsNoTracking()
                                    .AnyAsync(c => c.CategoryName == category.CategoryName
                                               && c.CategoryID != category.CategoryID))
                    {
                        throw new InvalidOperationException($"Category with the name {category.CategoryName} already exists");
                    }

                    var dbCategory = await context.Categories.FindAsync(category.CategoryID);
                    dbCategory.CategoryName = category.CategoryName;
                    dbCategory.ShowOnNavBar = category.ShowOnNavBar;
                }
                await context.SaveChangesAsync();
                return category;

            });





        }

        public async Task<Category?> GetCategoryBySlugAsync(string slug) =>
               await ExecuteOnContext(async context =>
               await context.Categories
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.Slug == slug));

    }
}

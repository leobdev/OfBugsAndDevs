using OfBugsAndDevs.Data.Entities;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category[]> GetCategoriesAsync();
        Task<Category?> GetCategoryBySlugAsync(string slug);
        Task<Category> SaveCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryID, string userID);
    }
}
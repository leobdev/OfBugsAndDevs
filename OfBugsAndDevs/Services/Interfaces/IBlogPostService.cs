using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface IBlogPostService
    {
        public Task<BlogPost[]> GetFeaturedBlogPostsAsync(int count, int categoryID = 0);
        public Task<BlogPost[]> GetPopularBlogPostsAsync(int count, int categoryID = 0);
        public Task<BlogPost[]> GetRecentBlogPostsAsync(int count, int categoryID = 0);
        public Task<DetailedPageModel> GetBlogPostBySlugAsync(string slug);
        
    }
}

using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface IBlogPostAdminService
    {
        public Task<PagedResult<BlogPost>> GetBlogPostsAsync(int startIndex, int pageSize);
        public Task<BlogPost?> GetBlogPostByIdAsync(int id);
        public Task<BlogPost> SaveBlogPostAsync(BlogPost blogPost, string userID);
    }
}

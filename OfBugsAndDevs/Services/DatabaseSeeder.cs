using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Data;

namespace OfBugsAndDevs.Services
{
    public class DatabaseSeeder
    {
        private readonly ApplicationDbContext _context;

        public DatabaseSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!_context.BlogPosts.Any())
            {
                _context.BlogPosts.Add(new BlogPost
                {
                    Title = "Welcome to the Blog",
                    Slug = "welcome-to-the-blog",
                    Image = "welcome.jpg",
                    Introduction = "An introduction to our blog platform.",
                    Content = "This is the content of the first blog post.",
                    CategoryID = 1, // "C#"
                    UserID = "f1547af3-db63-4b06-b7be-533fd43cee00", // Replace dynamically or with admin seeding logic
                    IsPublished = true,
                    ViewCount = 0,
                    IsFeatured = true,
                    Created = DateTime.UtcNow,
                    Published = DateTime.UtcNow,
                    
                });

                await _context.SaveChangesAsync();
            }
        }
    }

}

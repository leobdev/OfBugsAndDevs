using OfBugsAndDevs.Data.Entities;

namespace OfBugsAndDevs.Models
{
    public record PagedResult<TResult>(TResult[] records, int totalCount);

    public record DetailedPageModel(BlogPost BlogPost, BlogPost[] RelatedBlogPost)
    {
        public static DetailedPageModel Empty() => new DetailedPageModel(default, []);
        public bool IsEmpty => BlogPost is null;
    }
}

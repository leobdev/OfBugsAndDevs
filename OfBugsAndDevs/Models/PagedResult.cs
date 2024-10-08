namespace OfBugsAndDevs.Models
{
    public record PagedResult<TResult>(TResult[] records, int totalCount);

    public class PagedResult
    {
        
    }
}

using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<string?> SubscribeAsync(Subscriber subscriber);
        public Task<PagedResult<Subscriber>> GetSubscribersAsync(int startIndex = 0, int pageSize = int.MaxValue);

    }
}

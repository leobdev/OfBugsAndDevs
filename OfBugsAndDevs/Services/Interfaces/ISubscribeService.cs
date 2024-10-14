using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<string?> SubscribeAsync(Subscriber subscriber);
        public Task<PagedResult<Subscriber>> GetSubscribersAsync(int startIndex, int pagSize);

    }
}

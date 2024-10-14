using OfBugsAndDevs.Data.Entities;

namespace OfBugsAndDevs.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<string?> SubscribeAsync(Subscriber subscriber);

    }
}

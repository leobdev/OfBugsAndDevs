using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Services.Interfaces;

namespace OfBugsAndDevs.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public SubscribeService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<string?> SubscribeAsync(Subscriber subscriber)
        {
            using var context = _contextFactory.CreateDbContext();
            var IsAlreadySubscribed = await context.Subscribers
                                                   .AsNoTracking()
                                                   .AnyAsync(s => s.Email == subscriber.Email);
            if(IsAlreadySubscribed)
            {
                return "You are already subscribed.";
            }
            subscriber.SSubscribedOn = DateTime.UtcNow;
            await context.Subscribers.AddAsync(subscriber);
            await context.SaveChangesAsync();
            return null;
        }
    }
}

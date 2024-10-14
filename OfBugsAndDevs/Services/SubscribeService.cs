using Microsoft.EntityFrameworkCore;
using OfBugsAndDevs.Data;
using OfBugsAndDevs.Data.Entities;
using OfBugsAndDevs.Models;
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

        public async Task<PagedResult<Subscriber>> GetSubscribersAsync(int startIndex, int pagSize)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.Subscribers
                                .AsNoTracking()
                                .OrderByDescending(s => s.SSubscribedOn);

            var totalCount = await query.CountAsync();
                               
            var records = await query.Skip(startIndex)
                                     .Take(pagSize)
                                     .ToArrayAsync();

            return new PagedResult<Subscriber>(records, totalCount);
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

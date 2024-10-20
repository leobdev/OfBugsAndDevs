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

        public async Task<PagedResult<Subscriber>> GetSubscribersAsync(int startIndex = 0, int pageSize = int.MaxValue)
        {
            using var context = _contextFactory.CreateDbContext();
            IOrderedQueryable<Subscriber> query = context.Subscribers
                                                          .AsNoTracking()
                                                          .OrderByDescending(s => s.SSubscribedOn); // Updated type here

            // Get the total count of subscribers
            var totalCount = await query.CountAsync();

            // Apply pagination only if pageSize is less than int.MaxValue
            if (pageSize != int.MaxValue)
            {
                query = (IOrderedQueryable<Subscriber>)query.Skip(startIndex).Take(pageSize);
            }

            var records = await query.ToArrayAsync();

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

using Microsoft.EntityFrameworkCore;
using MW.Domain.Entities;
using MW.Identity;
using MW.Identity.Models;
using MW.Identity.Repositories;


namespace MW.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        // Assuming you have a data context to manage subscriptions
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubscriptionAsync(string userId, string channelId)
        {
            // Add logic to add a subscription to the database
            var subscription = new Subscription { UserId = userId, ChannelId = channelId };
            _context.Subscriptions.Add(subscription);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveSubscriptionAsync(string userId, string channelId)
        {
            // Add logic to remove a subscription from the database
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.ChannelId == channelId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}
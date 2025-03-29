using Microsoft.EntityFrameworkCore;
using MW.Application.Contracts.Interfaces;
using MW.Domain.Entities;
using MW.Identity;

public class SubscriptionService : ISubscriptionService
{
    private readonly ApplicationDbContext _dbContext;

    public SubscriptionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SubscribeAsync(string userId, string channelId)
    {
        if (userId == channelId)
            return false;

        var alreadySubscribed = await _dbContext.Subscriptions
            .AnyAsync(s => s.UserId == userId && s.ChannelId == channelId);

        if (alreadySubscribed)
            return false;

        var subscription = new Subscription
        {
            UserId = userId,
            ChannelId = channelId,
            SubscribedAt = DateTime.UtcNow
        };

        _dbContext.Subscriptions.Add(subscription);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnsubscribeAsync(string userId, string channelId)
    {
        var subscription = await _dbContext.Subscriptions
            .FirstOrDefaultAsync(s => s.UserId == userId && s.ChannelId == channelId);

        if (subscription == null)
            return false;

        _dbContext.Subscriptions.Remove(subscription);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsSubscribedAsync(string userId, string channelId)
    {
        return await _dbContext.Subscriptions
            .AnyAsync(s => s.UserId == userId && s.ChannelId == channelId);
    }

    // ✅ Add this:
    public async Task<int> GetSubscriberCountAsync(string channelId)
    {
        return await _dbContext.Subscriptions
            .CountAsync(s => s.ChannelId == channelId);
    }
}
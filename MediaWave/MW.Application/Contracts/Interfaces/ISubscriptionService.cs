namespace MW.Application.Contracts.Interfaces
{
    public interface ISubscriptionService
    {
        Task<bool> SubscribeAsync(string userId, string channelId);
        Task<bool> UnsubscribeAsync(string userId, string channelId);
    }
}
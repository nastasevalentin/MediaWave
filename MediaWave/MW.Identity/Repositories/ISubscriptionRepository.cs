namespace MW.Identity.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<bool> AddSubscriptionAsync(string userId, string channelId);
        Task<bool> RemoveSubscriptionAsync(string userId, string channelId);
    }
}
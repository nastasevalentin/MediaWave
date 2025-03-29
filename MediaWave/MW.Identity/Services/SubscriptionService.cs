
using MW.Application.Contracts.Interfaces;
using MW.Identity.Repositories;

namespace MW.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<bool> SubscribeAsync(string userId, string channelId)
        {
            // Add logic to subscribe the user to the channel
            var result = await _subscriptionRepository.AddSubscriptionAsync(userId, channelId);
            return result;
        }

        public async Task<bool> UnsubscribeAsync(string userId, string channelId)
        {
            // Add logic to unsubscribe the user from the channel
            var result = await _subscriptionRepository.RemoveSubscriptionAsync(userId, channelId);
            return result;
        }
    }
}
using MW.Application.Models;

namespace MW.Application.Contracts.Interfaces;

public interface IProfileService
{
    Task<UserProfileDto?> GetUserProfileAsync(string userId);
    Task<bool> IsSubscribedAsync(string viewerId, string profileOwnerId);
    Task<int> GetSubscriberCountAsync(string profileOwnerId);
}

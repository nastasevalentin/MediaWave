// MW.Application.Services/ProfileService.cs

using MW.Application.Contracts.Interfaces;
using MW.Application.Models;

// MW.Application.Services/ProfileService.cs

using MW.Application.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using MW.Identity;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ISubscriptionService _subscriptionService;

    public ProfileService(ApplicationDbContext dbContext, ISubscriptionService subscriptionService)
    {
        _dbContext = dbContext;
        _subscriptionService = subscriptionService;
    }

    public async Task<UserProfileDto?> GetUserProfileAsync(string userId)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new UserProfileDto
            {
                UserId = u.Id,
                Username = u.UserName,
                Email = u.Email
            })
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<bool> IsSubscribedAsync(string viewerId, string profileOwnerId)
    {
        return await _subscriptionService.IsSubscribedAsync(viewerId, profileOwnerId);
    }

    public async Task<int> GetSubscriberCountAsync(string profileOwnerId)
    {
        return await _subscriptionService.GetSubscriberCountAsync(profileOwnerId);
    }
}

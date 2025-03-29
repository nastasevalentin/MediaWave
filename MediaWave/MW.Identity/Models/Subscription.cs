using MW.Identity.Models;

namespace MW.Domain.Entities;

public class Subscription
{
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string ChannelId { get; set; }

    public DateTime SubscribedAt { get; set; }
}
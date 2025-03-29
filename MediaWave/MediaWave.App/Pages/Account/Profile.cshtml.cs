using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MW.Identity;
using System.Threading.Tasks;
using MW.Application.Contracts.Interfaces;

public class ProfileModel : PageModel
{
    private readonly IProfileService _profileService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public string? Username { get; set; }
    public string? Email { get; set; }
    public bool IsSubscribed { get; set; }
    public int SubscriberCount { get; set; }

    public string? ProfileUserId { get; set; }
    public string? LoggedInUserId => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public ProfileModel(IProfileService profileService, IHttpContextAccessor httpContextAccessor)
    {
        _profileService = profileService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> OnGetAsync(string id)
    {
        ProfileUserId = id;

        var profile = await _profileService.GetUserProfileAsync(id);
        if (profile == null)
        {
            return NotFound();
        }

        Username = profile.Username;
        Email = profile.Email;
        SubscriberCount = await _profileService.GetSubscriberCountAsync(id);

        if (LoggedInUserId != null && LoggedInUserId != id)
        {
            IsSubscribed = await _profileService.IsSubscribedAsync(LoggedInUserId, id);
        }

        return Page();
    }
}

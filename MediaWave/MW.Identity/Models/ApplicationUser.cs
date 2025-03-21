using Microsoft.AspNetCore.Identity;

namespace MW.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
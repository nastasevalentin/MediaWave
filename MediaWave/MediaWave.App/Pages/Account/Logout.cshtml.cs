using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MediaWave.App.Pages.Account;

public class Logout : PageModel
{
    public IActionResult OnGet()
    {
        // ✅ Delete the JWT cookie manually
        Response.Cookies.Delete("Authorization");

        // Optional: clear the security context
        HttpContext.User = new System.Security.Claims.ClaimsPrincipal();

        return RedirectToPage("/Index");
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MW.Identity;
using System.Threading.Tasks;

public class ProfileModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ProfileModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty(SupportsGet = true)]
    public string Id { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _context.Users.FindAsync(Id);
        if (user == null) return NotFound();

        Username = user.UserName;
        Email = user.Email;

        return Page();
    }
}
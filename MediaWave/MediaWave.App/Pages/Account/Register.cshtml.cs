using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MW.Application.Models.Identity; // adjust to your actual namespace

namespace MediaWave.App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public RegistrationModel Input { get; set; } = new();

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("API");
            var response = await client.PostAsJsonAsync("/api/v1/authentication/register", Input);

            if (response.IsSuccessStatusCode)
            {
                SuccessMessage = "Account created successfully! You can now log in.";
                // Optional: redirect to Login page instead of showing the form again
                // return RedirectToPage("/Account/Login");
                return Page();
            }

            ErrorMessage = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, ErrorMessage ?? "Registration failed.");
            return Page();
        }
    }
}
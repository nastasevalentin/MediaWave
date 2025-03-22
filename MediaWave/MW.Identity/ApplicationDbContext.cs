
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MW.Identity.Models;

namespace MW.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IdentityDB;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;");
        }
        
        // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        // {
        // }
    }
}
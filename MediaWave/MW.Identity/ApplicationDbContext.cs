
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MW.Domain.Entities;
using MW.Identity.Models;

namespace MW.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {        
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.UserId, s.ChannelId });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IdentityDB;User Id=sa;Password=YourStrong@Password123;TrustServerCertificate=True;");
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
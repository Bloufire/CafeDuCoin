using CafeDuCoinAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CafeDuCoinAPI
{
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser { }

    public class CafeDuCoinContext : IdentityDbContext<ApplicationUser>
    {
        public CafeDuCoinContext(DbContextOptions<CafeDuCoinContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasKey(e => e.ID);
            modelBuilder.Entity<Loan>().HasKey(e => e.ID);
        }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
    }
}

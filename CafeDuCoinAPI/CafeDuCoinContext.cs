using CafeDuCoinAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CafeDuCoinAPI
{
    // Define the ApplicationUser class inheriting from IdentityUser
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser { }

    // Define the CafeDuCoinContext class inheriting from IdentityDbContext
    public class CafeDuCoinContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor to accept DbContextOptions and pass them to the base constructor
        public CafeDuCoinContext(DbContextOptions<CafeDuCoinContext> options) : base(options) { }

        // Override OnModelCreating method to configure the model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method for Identity configuration
            base.OnModelCreating(modelBuilder);

            // Configure primary keys for Game and Loan entities
            modelBuilder.Entity<Game>().HasKey(e => e.ID);
            modelBuilder.Entity<Loan>().HasKey(e => e.ID);
        }

        // Define DbSet properties for the Game and Loan entities
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;
    }
}

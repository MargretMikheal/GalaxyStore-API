using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GalaxyStore.Domain.Models;

using Microsoft.AspNetCore.Identity;
using System.Reflection;
using GalaxyStore.Domain.Identity;

namespace GalaxyStore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "Account");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "Account");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Account");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Account");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Account");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Account");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Account");

            modelBuilder.HasDefaultSchema("Galaxy");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

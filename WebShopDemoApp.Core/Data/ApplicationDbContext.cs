using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopDemoApp.Core.Data.Models;
using WebShopDemoApp.Core.Data.Models.Account;

namespace WebShopDemoApp.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<Product>()
            //    .Property(p => p.IsActive)
            //    .HasDefaultValue(true);

            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }


    }
}
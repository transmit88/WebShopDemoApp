using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopDemoApp.Core.Data.Models;

namespace WebShopDemoApp.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            


            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
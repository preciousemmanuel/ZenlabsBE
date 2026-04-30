using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using ZentekLabs.Models.Domain;

namespace ZentekLabs.Data
{
    public class ZenteklabDbContext: DbContext
    {

        public ZenteklabDbContext(DbContextOptions<ZenteklabDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        }
}

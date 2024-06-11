using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class BethanysPieShopContext : DbContext
    {
        public BethanysPieShopContext(DbContextOptions<BethanysPieShopContext> options)
            : base(options) { }

        public DbSet<Pie> Pies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

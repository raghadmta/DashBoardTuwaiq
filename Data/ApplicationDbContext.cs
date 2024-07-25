using DashBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Damagedproducts> Damagedproducts { get; set; }
    }
}

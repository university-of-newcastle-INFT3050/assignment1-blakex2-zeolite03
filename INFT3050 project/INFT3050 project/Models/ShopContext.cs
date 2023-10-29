using INFT3050_project.Models.Product;
using INFT3050_project.Models.Product.Subgenre;
using Microsoft.EntityFrameworkCore;
using INFT3050_project.Models;
namespace INFT3050_project.Models
{
    //shop context class handles most information needed for the database and the relevant object
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product.Product> Product { get; set; } = null!;
        public DbSet<Genre> Genre { get; set; } = null!;

        public DbSet<Stocktake.Source> Sources { get; set; } = null!;
        public DbSet<Stocktake.Stocktake> Stocktake { get; set; } = null!;
        public DbSet<Book_Genre> Book_Genres { get; set; } = null!;
        public DbSet<Game_Genre> Game_Genres { get; set; } = null!;
        public DbSet<Movie_Genre> Movie_Genres { get; set; } = null!;
        public DbSet<User> User { get; set; }
        public DbSet<Order.Orders> Orders { get; set; } = null!;
        public DbSet<Order.TO> TO { get; set; } = null!;
        public DbSet<Order.ProductsInOrders> ProductsInOrders { get; set; }
        public DbSet<INFT3050_project.Models.Patrons> Patrons { get; set; } 
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stocktake.Stocktake>()
                .HasKey(s => s.itemid);
        }




    }
}

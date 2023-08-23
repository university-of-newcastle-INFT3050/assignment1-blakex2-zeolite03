using INFT3050_project.Models.Product;
using INFT3050_project.Models.Product.Subgenre;
using Microsoft.EntityFrameworkCore;
namespace INFT3050_project.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
           : base(options)
        { }

        public DbSet<Product.Product> Product { get; set; } = null!;
        public DbSet<Genre> Genre { get; set; } = null!;

       

        public DbSet<Book_Genre> Book_Genres { get; set; } = null!;
        public DbSet<Game_Genre> Game_Genres { get; set; } = null!;
        public DbSet<Movie_Genre> Movie_Genres { get; set; } = null!;

        
    

    }
}

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
        public DbSet<Book_Genre> Book_Genre { get; set; } = null!;
        public DbSet<Game_Genre> Game_Genre { get; set; } = null!;
        public DbSet<Movie_Genre> Movie_Genre { get; set; } = null!;

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product.Product>().HasData(
                new Product.Product { ID = 1, Name = "TEST 1", Author = "testauthor", Description = "testdescription",  },
                new Product.Product { ID = 2, Name = "TEST 2", Author = "testauthor2", Description = "testdescription2" }
            );


            modelBuilder.Entity<User>().HasData(
             new User { UserName = "testuser1", Email = "testuser1@mail.com", HashedPW = "pass1", Name = "testname1", IsAdmin = false, Salt = "salt1", UserId = 1},
             new User { UserName = "testuser2", Email = "testuser2@mail.com", HashedPW = "pass2", Name = "testname2", IsAdmin = true, Salt = "salt2" , UserId = 2}
             
         );
        }

    }
}

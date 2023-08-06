using INFT3050_project.Models.Product.Genre;
using INFT3050_project.Models.Product.Subgenre;
using INFT3050_project.Models.Product.Genre;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace INFT3050_project.Models.Product
{
    public class Product
    {
        public class Movie
        {
            
            public int ProductID { get; set; }

            [Required(ErrorMessage = "Please enter a name.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please enter a Author.")]
            public int? Author { get; set; }

            [Required(ErrorMessage = "Please enter a Decsription.")]
            public int? Description { get; set; }

            [Required(ErrorMessage = "Please enter a genre.")]
            public string GenreId { get; set; }
            public Genre Genre { get; set; }

            

            public Book_Genre Book_Genre;

            public Game_Genre Game_Genre;

            public Movie_Genre Movie_Genre;



           
        }
}

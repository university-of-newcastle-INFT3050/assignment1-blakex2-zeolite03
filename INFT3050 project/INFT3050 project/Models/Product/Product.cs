using INFT3050_project.Models.Product.Subgenre;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.Models.Product
{
    public class Product
    {

            public int ProductID { get; set; }

            [Required(ErrorMessage = "Please enter a name.")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Please enter a Author.")]
            public int? Author { get; set; }

            [Required(ErrorMessage = "Please enter a Decsription.")]
            public int? Description { get; set; }

            [Required(ErrorMessage = "Please enter a genre.")]
            public Genre Genre { get; set; }

            public DateTime Published { get; set; }

            public User LastUpdatedBy { get; set; }

            public DateTime LastUpdated { get; set; }


        }
    }
}

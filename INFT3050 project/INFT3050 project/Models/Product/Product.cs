using INFT3050_project.Models.Product.Subgenre;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INFT3050_project.Models.Product
{
    //info on the product
    public class Product
    {
        // some text validation 
        // most text validation is within the view models 
            public int ID { get; set; }

            [Required(ErrorMessage = "Please enter a name.")]
            public string? Name { get; set; }

        [ForeignKey("GenreLink")]
            public int genre { get; set; }
        [ValidateNever]
            public Genre GenreLink { get; set; }

        public int subGenre { get; set; }

            [Required(ErrorMessage = "Please enter a Author.")]
            public String? Author { get; set; }

            [Required(ErrorMessage = "Please enter a Decsription.")]
            public String? Description { get; set; }

            [Required(ErrorMessage = "Please enter a genre.")]
            //public int GenreID { get; set; }
            public DateTime Published { get; set; }
        //commented out because didnt get timte to implement
            //public User LastUpdatedBy { get; set; }

            //public DateTime LastUpdated { get; set; }


    }
    
}

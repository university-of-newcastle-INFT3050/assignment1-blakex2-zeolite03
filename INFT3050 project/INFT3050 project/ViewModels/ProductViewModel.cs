using INFT3050_project.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class ProductViewModel
    {
        // this is used to send data from homecontroller to homepage view
        public Product Product { get; set; }
        public SubGenreViewModel SubGenreViewModel { get; set; }
       
    }
}

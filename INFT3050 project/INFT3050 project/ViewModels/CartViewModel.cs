using INFT3050_project.Models.Product;
using INFT3050_project.Models;
using INFT3050_project.Models.Stocktake;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{     
    public class CartViewModel
    {
        // these is where data is sent from cart controller to cardetails
        public List<Product> Products { get; set; }
        public List<Stocktake> Stocktakes { get; set; }

    }
}

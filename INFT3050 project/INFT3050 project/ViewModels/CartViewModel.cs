using INFT3050_project.Models.Product;
using INFT3050_project.Models;
using INFT3050_project.Models.Stocktake;

namespace INFT3050_project.ViewModels
{     
    public class CartViewModel
    {
        public List<Product> Products { get; set; }
        public List<Stocktake> Stocktakes { get; set; }

    }
}

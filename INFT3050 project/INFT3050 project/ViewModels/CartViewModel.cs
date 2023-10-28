using INFT3050_project.Models.Product;
using INFT3050_project.Models;
namespace INFT3050_project.ViewModels
{     
    public class CartViewModel
    {
        public List<Product> Products { get; set; }
        public User User { get; set; }
    }
}

using INFT3050_project.Models.Product;

namespace INFT3050_project.ViewModels
{
    public class HomePageViewModel
    {
      
            public string UserId { get; set; }
             public string Name { get; set; }
            public List<Product> Products { get; set; }
            public List<Genre> Genres { get; set; }
        
    }
}

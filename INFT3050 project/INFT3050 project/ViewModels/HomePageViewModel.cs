using INFT3050_project.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace INFT3050_project.ViewModels
{
    public class HomePageViewModel
    {
      //this to put infomation from home controleer to home page to display list of objects
      // userid and name was for testing if session worked
            public string UserId { get; set; }
             public string Name { get; set; }
            public List<Product> Products { get; set; }
            public List<Genre> Genres { get; set; }
        
    }
}

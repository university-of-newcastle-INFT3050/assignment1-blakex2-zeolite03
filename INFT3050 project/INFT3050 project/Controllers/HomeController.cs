using INFT3050_project.Models;
using INFT3050_project.Models.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INFT3050_project.Controllers
{
    public class HomeController : Controller
    {
        


        private ShopContext context;

        public HomeController(ShopContext ctx)
        {
            context = ctx;
        }

       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            var products = context.Product;
            
            return View(products);
        }

        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
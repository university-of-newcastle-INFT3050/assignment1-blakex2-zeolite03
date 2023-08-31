using INFT3050_project.Models;
using INFT3050_project.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var genres = context.Genre.ToList();
            var _products = context.Product.OrderBy(t => t.Name).ToList();

            return View(_products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            var genres = context.Genre.ToList();
            var products = context.Product.ToList();
            
            return View(products);
        }

        public async Task OnPostAsync()
        {
            var searchString = Request.Form["searchString"];
            //= await context.Product.Where(x => x.Name.Contains(searchString)).ToListAsync();
            

        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();

        }

        public IActionResult EditorView()
        {
            var genres = context.Genre.ToList();
            var products = context.Product.ToList();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
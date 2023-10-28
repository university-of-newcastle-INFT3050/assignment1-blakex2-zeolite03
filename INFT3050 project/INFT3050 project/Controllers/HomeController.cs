using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.Models.Stocktake;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using static INFT3050_project.ViewModels.HomePageViewModel;

namespace INFT3050_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;



        private ShopContext context;

        List<Product> productlist = new List<Product> { };
        public HomeController(ShopContext ctx)
        {
            context = ctx;
            
        }

        //public IActionResult PatronDashboard()
        //{
        //    var patronId = HttpContext.Session.GetInt32("PatronId");

        //    if (patronId.HasValue)
        //    {
        //        var patron = GetPatronFromDatabase(patronId.Value);
        //        return View("PatronDashboard", patron); // Return the PatronDashboard view
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //}
        //public Patrons GetPatronFromDatabase(int patronId)
        //{
        //    // Assuming you have a DbSet for Patrons in your ShopContext
        //    Patrons patron = context.Patrons.FirstOrDefault(p => p.UserId == patronId);
        //    return patron;
        //}
        public IActionResult Index()
        {

            //var genres = context.Genre.ToList();
            //var _products = context.Product.OrderBy(t => t.Name).ToList();


            //return View(_products);
            var viewModel = new HomePageViewModel();

            // Retrieve the user ID from the session (if needed)
            if (HttpContext.Session.GetString("UserId") != null)
            {
                viewModel.UserId = HttpContext.Session.GetString("UserId");
               
                
            }

            viewModel.Products = context.Product
                .OrderBy(product => product.Name)
                .ToList();

            viewModel.Genres = context.Genre
                .ToList();

            return View(viewModel);

        }

        public IActionResult Details(int id)
        {
            ProductViewModel model = new ProductViewModel()
            {
                Product = context.Product.Find(id),
                SubGenreViewModel = ShopManager.GetViewModel(context)
            };

            return View(model);
        }
        public IActionResult AddToCart(int id)
        {
            var user = HttpContext.Session.GetString("UserId");
            if (user != null)
            {
                var StoreCart = HttpContext.Session.GetString("Cart");
                //
                var Cart = string.IsNullOrEmpty(StoreCart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(StoreCart);
                
                var product = context.Product.FirstOrDefault(u => u.ID == id);
                Cart.Add(product);
                var newCart = JsonConvert.SerializeObject(Cart);
                HttpContext.Session.SetString("Cart", newCart);

                
                //HttpContext.Session.Set("CartProducts", productlist);
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return RedirectToAction("LoginPage", "Login");
            }
            
        }            




            

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage(string search)
        {
            var viewModel = new HomePageViewModel();
            if (HttpContext.Session.GetString("UserId") != null)
            {
                viewModel.UserId = HttpContext.Session.GetString("UserId");

                var patron = context.Patrons.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);
                if (patron != null)
                {
                    viewModel.Name = patron.Name;
                }
                var user = context.User.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);
                if(user != null)
                {
                    viewModel.Name = user.Name;
                }

            }
            viewModel.Products = context.Product.Include("GenreLink").ToList();
            
            if (!string.IsNullOrWhiteSpace(search))
            {
                viewModel.Products = viewModel.Products.Where(x => x.Name.Contains(search)).ToList();
            }
            
          return View(viewModel);
        }

        public async Task OnPostAsync()
        {
            var searchString = Request.Form["searchString"];
            var products = await context.Product.Where(x => x.Name.Contains(searchString)).ToListAsync();


        }
        public IActionResult Test()
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

        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            Console.WriteLine($"Search Term: {searchTerm}");

            var query = context.Product.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }
            var filteredProducts = query.ToList();

            return View("HomePage", filteredProducts);
        }
        



    }
}
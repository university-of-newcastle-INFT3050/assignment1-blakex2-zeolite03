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

//handles most of the homepage actions including displaying products
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


        //the first page seen when using the site
        //displays list of products to the user
        public IActionResult Index()
        {

            //creates new view model
            var viewModel = new HomePageViewModel();

            //retrieve the user ID from the session (if needed)
            if (HttpContext.Session.GetString("UserId") != null)
            {
                viewModel.UserId = HttpContext.Session.GetString("UserId");
               
                
            }
            //creates a list of all products
            viewModel.Products = context.Product
                .OrderBy(product => product.Name)
                .ToList();
            //creates a list of the genre's
            viewModel.Genres = context.Genre
                .ToList();

            return View(viewModel);

        }
        //displays detailed information about a product
        public IActionResult Details(int id)
        {
            //creates a view model based on the inputted product id the user selected
            ProductViewModel model = new ProductViewModel()
            {
                Product = context.Product.Find(id),
                SubGenreViewModel = ShopManager.GetViewModel(context)
            };
            //returns the product via a view model
            return View(model);
        }
        //adds a product to the shopping cart
        public IActionResult AddToCart(int id)
        {
            //takes in the users id from the session
            var user = HttpContext.Session.GetString("UserId");
            //checks if there is a user
            if (user != null)
            {
                //gets previous information about the car from the session
                var StoreCart = HttpContext.Session.GetString("Cart");
                //
                //creates a list based on the sessions json info
                var Cart = string.IsNullOrEmpty(StoreCart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(StoreCart);
                //gets the current product selected and finds the product object via its id
                var product = context.Product.FirstOrDefault(u => u.ID == id);
                //add the product to the list
                Cart.Add(product);
                //converts the list back to a json and them sets it in the session again
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
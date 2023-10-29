using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050_project.Controllers
{
    //handles the product editing 
    public class ProductController : Controller
    {
        public ShopContext context;

        //creates the shopcontext for the controller
        public ProductController(ShopContext ctx)
        {
            this.context = ctx;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var genres = context.Genre.ToList();
            var _products = context.Product.OrderBy(t => t.Name).ToList();
            return View(_products);
        }
        //shows the homepage for the product editing
        public IActionResult HomePage()
        {
            var genres = context.Genre.ToList();
            var products = context.Product.ToList();

            return View(products);
        }
        //does the same but for different call
        public IActionResult EditorView()
        {
            var genres = context.Genre.ToList();
            var products = context.Product.ToList();
            return View(products);
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

      
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //generates a page based on the selected product
        public ActionResult Edit(int id)
        {
            //creates a new view model for the product
            ProductViewModel model = new ProductViewModel()
            {
                //finds the product based on the associated id and adds it to the view model
                Product = context.Product.Find(id),
                SubGenreViewModel = ShopManager.GetViewModel(context)
            };
            //returns the product info to the page
            return View(model);
        }

       

            //edits the product
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var name = Request.Form["Name"];
            try
            {
                //checks if info on edited product is valid for requirements
                if (ModelState.IsValid)
                {
                    //if the product has an id of 0, it is a new product
                    if (product.ID == 0)
                        context.Product.Add(product);
                    else
                    //therwise, it will update a product based on the id supplied and the info inputted
                        context.Product.Update(product);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Action = (product.ID == 0) ? "Add" : "Edit";
                    ViewBag.Genres = context.Product.OrderBy(t => t.Name).ToList();
                    return View(product);
                }
                //logic to save to db here
                return RedirectToRoute(new { controller = "Product", action = "EditorView" });
                
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

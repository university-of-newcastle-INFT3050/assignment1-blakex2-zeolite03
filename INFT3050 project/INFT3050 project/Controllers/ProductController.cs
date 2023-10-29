using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
//Eastwood did the editing, Eveleigh did the adding and deleting items
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
       


        // GET: ProductController/Details/5
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


        // GET: ProductController/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            //finding product in the db based on id
            var product = context.Product.Find(id);
            //also finding the stock take and cant delete product without because  product without deleting the stock that points to it first.
            var stock = context.Stocktake.Where(u=>u.ProductId == id);
            if (stock != null)
            {
                foreach (var item in stock)
                {
                    //removes the stock
                    context.Stocktake.Remove(item);
                    context.SaveChanges();
                }

                }
            //removes the product.
            context.Product.Remove(product);
            context.SaveChanges();

            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        public IActionResult Add()

        {
            
            return View();
        }


        [HttpPost]
        public IActionResult AddItem(AddItemViewModel model) 
        {
            //just takes the infomaion form the view and makes a new product
            //because it doesnt add a genre it display on the views
            //if we had more time this is something we would do.
            var NewItem = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Author = model.Author

            };

            return RedirectToAction("HomePage", "Home");
        }



    }
}

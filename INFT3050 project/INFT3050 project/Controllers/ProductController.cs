using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050_project.Controllers
{
    public class ProductController : Controller
    {
        public ShopContext context;
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

        public IActionResult HomePage()
        {
            var genres = context.Genre.ToList();
            var products = context.Product.ToList();

            return View(products);
        }

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

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ProductViewModel model = new ProductViewModel()
            {
                Product = context.Product.Find(id),
                SubGenreViewModel = ShopManager.GetViewModel(context)
            };
            
            return View(model);
        }

       

            // POST: ProductController/Edit/5
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            var name = Request.Form["Name"];
            try
            {

                if (ModelState.IsValid)
                {
                    if (product.ID == 0)
                        context.Product.Add(product);
                    else
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
            var product = context.Product.Find(id);
            var stock = context.Stocktake.Where(u=>u.ProductId == id);
            if (stock != null)
            {
                foreach (var item in stock)
                {
                    context.Stocktake.Remove(item);
                    context.SaveChanges();
                }

                }
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

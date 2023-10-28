using INFT3050_project.Models.Stocktake;
using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Newtonsoft.Json;
using INFT3050_project.Models.Order;
using System.Composition;
using System.Net;
using static NuGet.Packaging.PackagingConstants;

public class CartController : Controller
{
    private ShopContext context;

    public CartController(ShopContext ctx)
    {
        context = ctx;
    }


    public IActionResult Checkout()
    {

        return View();
    }

    public IActionResult CartDetails()
    {
        string products = HttpContext.Session.GetString("productlist");

        // Split the string into a list of strings
        var Cart = HttpContext.Session.GetString("Cart");
        var Cart2 = string.IsNullOrEmpty(Cart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(Cart);
        List<Stocktake> stocktakeList = new List<Stocktake>();
        foreach(Product product in Cart2)
        {
            int productId = product.ID;
            Stocktake stock = context.Stocktake.FirstOrDefault(s => s.ProductId == productId);
            stocktakeList.Add(stock);
            var OrderProducts = new ProductsInOrders
            {
                ProductId = product.ID,
             };
        }
        var viewModel = new CartViewModel
        {
            Stocktakes = stocktakeList,
            Products = Cart2
        };
        

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Checkout(OrderViewModel model)
    {
        var user = HttpContext.Session.GetString("UserId");
        int userid = int.Parse(user);
        var to = new TO
        {
            customerID = 43,
            PatronId = userid,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            StreetAddress = model.address,
            PostCode = model.postcode,
            Suburb = model.Suburb,
            CardNumber = model.CardNumber,
            CardOwner = model.CardOwner,
            Expiry = model.Expiry,
            CVV = model.CVV

        };
        var order = new Orders
        {
           // customer = to.customerID, // Corrected property name
            StreetAddress = model.address,
            postcode = model.postcode,
            Suburb = model.Suburb,
            state = model.state
        };
        context.TO.Add(to);
        context.Orders.Add(order);
        //context.SaveChanges();

        return RedirectToAction("HomePage", "Home");

        
    }

    public IActionResult Orders()
    {
        var user = HttpContext.Session.GetString("UserId");
        //reference this
        
        List<Orders> orders = new List<Orders>();
        var to = context.TO.Where(u =>u.PatronId.ToString() == user).ToList();
        foreach(var item in to)
        {
            orders = context.Orders.Where(u => u.customer == item.customerID).ToList();
        }
        var NewOrder = new OrderScreenViewModel();
        NewOrder.order = orders;

        
        return View(NewOrder);
    }
}

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

//the controller for handling all shopping cart actions
// both eveleigh and Eastwood worked on the cart together.
public class CartController : Controller
{
    private ShopContext context;

    //creates the shopcontext for the controller
    public CartController(ShopContext ctx)
    {
        context = ctx;
    }

    //unimplempented
    public IActionResult Checkout()
    {

        return View();
    }
    //shows the details of the cart created by the user/patron
    public IActionResult CartDetails()
    {
        //other half is in the home controller 
        //tldr takes the session from home deserialises it so we can get the list of products which we match to the stocktake and send both in a viewmodel to the view,


        //calls the session to make a string that conatins a list of the products ID's
        string products = HttpContext.Session.GetString("productlist");

        // Split the string into a list of strings
        var Cart = HttpContext.Session.GetString("Cart");
        var Cart2 = string.IsNullOrEmpty(Cart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(Cart);
        //creates a list for the stock take object and compares the product id to the stocktake table to find matching stock take objects.
        //takes these matching objects and puts them into a list
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
        //updates the view model based on this info
        var viewModel = new CartViewModel
        {
            Stocktakes = stocktakeList,
            Products = Cart2
        };
        
        //returns the view with this info
        return View(viewModel);
    }

    //not properly implemented
    [HttpPost]
    public IActionResult Checkout(OrderViewModel model)
    {
        // doesnt really work 
        //from the model create a new TO and new Order
        //test variables for adding to database
        var user = HttpContext.Session.GetString("UserId");
        int userid = int.Parse(user);
        var to = new TO
        {
            //just a placeholder this isnt fully finished by the end of the project
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
    //shows the user the orders
    //unimplemented
    public IActionResult Orders()
    {
        //calls userid info
        var user = HttpContext.Session.GetString("UserId");
        
        //creates a new list to store order
        List<Orders> orders = new List<Orders>();
        //goes through the database to find the patrons order info 
        var to = context.TO.Where(u =>u.PatronId.ToString() == user).ToList();
        foreach(var item in to)
        {
            //adds the order info
            orders = context.Orders.Where(u => u.customer == item.customerID).ToList();
        }
        var NewOrder = new OrderScreenViewModel();
        NewOrder.order = orders;

        //returns the info to the page
        return View(NewOrder);
    }
}

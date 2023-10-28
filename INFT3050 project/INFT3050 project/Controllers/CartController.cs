using INFT3050_project.Models.Stocktake;
using INFT3050_project.Models;
using Microsoft.AspNetCore.Mvc;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Newtonsoft.Json;

public class CartController : Controller
{
    private ShopContext context;

    public CartController(ShopContext ctx)
    {
        context = ctx;
    }

    public IActionResult CartDetails()
    {
        string productlistString = HttpContext.Session.GetString("productlist");

        // Split the string into a list of strings
        var currentCart = HttpContext.Session.GetString("Cart");
        var cart = string.IsNullOrEmpty(currentCart) ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(currentCart);
        List<Stocktake> stocktakeList = new List<Stocktake>();
        foreach(Product product in cart)
        {
            int productId = product.ID;
            Stocktake stockin = context.Stocktake.FirstOrDefault(s => s.ProductId == productId);
            stocktakeList.Add(stockin);
        }
        var viewModel = new CartViewModel
        {
            Stocktakes = stocktakeList,
            Products = cart
        }; 


        return View(viewModel);
    }
}

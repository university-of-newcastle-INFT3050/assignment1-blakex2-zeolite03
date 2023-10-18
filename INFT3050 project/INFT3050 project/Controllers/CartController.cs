using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace INFT3050_project.Controllers
{
    public class CartController : Controller
    {

        public ShopContext context;
        public CartController(ShopContext ctx)
        {
            this.context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CartDetails()
        {
            return View();
        }
    }
}

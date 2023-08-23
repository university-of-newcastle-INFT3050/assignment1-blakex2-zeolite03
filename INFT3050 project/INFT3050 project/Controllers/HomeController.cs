using System.Collections.Generic;
using System.Linq;
using INFT3050_project.Models;
using INFT3050_project.Models.Product;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult Index(string activeConf = "all", string activeDiv = "all")
        {
            // store selected conference and division IDs in view bag
            ViewBag.ActiveConf = activeConf;
            ViewBag.ActiveDiv = activeDiv;

            // store conferences and divisions from database in view bag
            ViewBag.Book_Genres = context.Book_Genres.ToList();
            ViewBag.Movie_Genres = context.Movie_Genres.ToList();
            ViewBag.Game_Genres = context.Game_Genres.ToList();
            ViewBag.Genres = context.Genres.ToList();

            // get teams - filter by conference and division
            IQueryable<Product> query = context.Products.OrderBy(t => t.Name);
            if (activeConf != "all")
                query = query.Where(
                    t => t.Genre.GenreID.ToLower() == activeConf.ToLower());
            //if (activeDiv != "all")
            //    query = query.Where(
            //        t => t.Division.DivisionID.ToLower() == activeDiv.ToLower());

            // pass list of teams to view as model
            var Products = query.ToList();
            return View(Products);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
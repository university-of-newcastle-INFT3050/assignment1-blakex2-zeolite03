﻿using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            var genres = context.Genre.ToList();
            var _products = context.Product.OrderBy(t => t.Name).ToList();

            return View(_products);
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult HomePage(string search)
        {
            var products = context.Product.Include("GenreLink").Where(x => true);
            //var a = products.ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(x => x.Name.Contains(search));
            }
            var b = products.ToList();
            //Ordering logic .OrderBy(x => x)
            //var genres = context.Genre.ToList();

            return View(b);
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
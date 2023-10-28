﻿using INFT3050_project.Models;
using INFT3050_project.Models.Managers;
using INFT3050_project.Models.Product;
using INFT3050_project.Models.Stocktake;
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
            // Retrieve the comma-separated string from the session
            string productlistString = HttpContext.Session.GetString("productlist");

            // Split the string into a list of strings
            List<string> productlistasstring = productlistString.Split(',').ToList();

            List<Stocktake> stocktakeList = new List<Stocktake>();
            foreach (var product in productlistasstring) 
            {
                Stocktake stockin = context.stocktakes.Find(product);
                stocktakeList.Add(stockin);
            }

            // take the list of stocktakelist and pass it to the view
            return View(stocktakeList);



            if (HttpContext.Session.GetString("UserId") != null)
            {

                var viewModel = new HomePageViewModel();
                viewModel.UserId = HttpContext.Session.GetString("UserId");


            }
            return View();
        }
    }
}

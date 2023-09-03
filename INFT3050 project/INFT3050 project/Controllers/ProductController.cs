﻿using INFT3050_project.Models;
using INFT3050_project.Models.Product;
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
            Product product = context.Product.Find(id);
            return View(product);
        }


       

            // POST: ProductController/Edit/5
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                //logic to save to db here
                return RedirectToRoute(new { controller = "Product", action = "EditorView" });
                
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
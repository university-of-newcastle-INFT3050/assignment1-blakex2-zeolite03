﻿using INFT3050_project.Models;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Text;
using NuGet.Packaging.Signing;

namespace INFT3050_project.Controllers
{
    public class LoginController : Controller
    {

        public ShopContext context;

        public LoginController(ShopContext ctx)
        {
            this.context = ctx;
        }
        // GET: LoginController
        public ActionResult index()
        {
            return View();
        }

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        public IActionResult CreateAccountPage()
        {


            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            Patrons patron = context.Patrons.Find(id);
            return View();
        }

        // POST: LoginController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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

        public ActionResult LoginPage(int id)
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(LoginViewModel model)
        {

            //if (ModelState.IsValid)
            //{

                if (IsValidUser(model.Email, model.HashPW))
                {
                    // Successful login
                    // Redirect to a protected area or perform other actions
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    // Invalid credentials, add a model error
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            //}

            // If we reach here, the ModelState is not valid, so redisplay the login form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        private bool IsValidUser(string email, string password)
        {
            bool IsValid;
            //this will search through all entities in User and return the user only if it matches the Username else it returns null
            var patron = context.Patrons.SingleOrDefault(u => u.Email == email);
            if (patron != null)
            {


                // Combine the stored salt with the entered password
                string saltedPassword = password + patron.Salt;

                string HashDB = patron.HashPW;
                // hashes the password sent by the user then checks it the same
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, HashDB);
                if (isPasswordCorrect)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }

            }
            else
            {
                IsValid = false;
            }

            return IsValid;
        }

        //public IActionResult LoginRecovery()
        //{
        //    return View();
        //}

        public IActionResult LoginRecovery(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var userList = context.User.ToList();

                foreach (var user in userList)
                {
                    if (user.Email.Equals(email))
                    {
                        return RedirectToAction("LoginRecoverySuccess", user);
                    }
                }
            }
            
            return View();
        }

        //public IActionResult LoginRecovery(bool mail_Verify)
        //{
        //    return RedirectToAction("RecoveryFailed");
        //}

        public IActionResult RecoveryFailed()
        {
            return View();
        }

        public ActionResult LoginRecoverySuccess(LoginViewModel model)
        {
            return View();
        }

        public ActionResult LoginRecoveryReset()
        {
            return View();
        }
   


        public IActionResult CreateAccount()
        {

            return View("CreateAccountPage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                //string saltedPassword = model.HashPW + salt;
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.HashPW, salt);
                var NewPatron = new Patrons
                {
                   
                    HashPW = HashedPassword,
                    Email = model.Email,
                    Name = model.Name,
                    Salt = salt
                    
                };
                context.Patrons.Add(NewPatron); 
                context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

            return View("CreateAccountPage", model);
        }
    }
}

using INFT3050_project.Models;
using INFT3050_project.Models.Product;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BCrypt.Net;
using System.Text;

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
            User user = context.User.Find(id);
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
           
            if (ModelState.IsValid)
            {

                if (IsValidUser(model.UserName, model.HashPW))
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
            }

            // If we reach here, the ModelState is not valid, so redisplay the login form
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        private bool IsValidUser(string username, string password)
        {
            bool IsValid;
            //this will search through all entities in User and return the user only if it matches the Username else it returns null
            var user = context.User.SingleOrDefault(u => u.UserName == username);
            if (user != null)
            {


                // Combine the stored salt with the entered password
                string saltedPassword = password + user.Salt;

                string HashDB = user.HashPW;
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

        public ActionResult Recovery()
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
                string saltedPassword = model.HashPW + salt;
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(saltedPassword);
                var newUser = new User
                {
                    UserName = model.UserName,
                    HashPW = HashedPassword,
                    Email = model.Email,
                    Name = model.Name,
                    
                };
                context.User.Add(newUser); 
                context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

            return View("CreateAccountPage", model);
        }
    }
}

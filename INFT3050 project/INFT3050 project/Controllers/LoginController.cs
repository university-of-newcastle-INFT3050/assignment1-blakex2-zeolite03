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
using NuGet.Packaging.Signing;
//using AspNetCore;
//Eveleigh did the login and create account while Eastwood did the login recovery.
namespace INFT3050_project.Controllers
{
    //handles the login process and pages
    public class LoginController : Controller
    {

        public ShopContext context;

        //sets the shopcontext for the controller
        public LoginController(ShopContext ctx)
        {
            this.context = ctx;
        }
       
        public ActionResult index()
        {
            return View();
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

      
        public ActionResult Create()
        {
            return View();
        }

        public IActionResult CreateAccountPage()
        {
            return View();
        }

        // POST: 
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

        //gets the account id an returns it
        public ActionResult Edit(int id)
        {
            Patrons patron = context.Patrons.Find(id);
            return View();
        }

        //edits the account information based on the form submitted and the associated account
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

        //shows the delete page
        public ActionResult Delete(int id)
        {
            return View();
        }

        //deletes the account based on the form and the associated account id
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
        //returns login page
        public ActionResult LoginPage(int id)
        {

            return View();
        }

        //handles the log in page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPage(LoginViewModel model)
        {
            //this is sending the email and password and seeing if there is a match to them
            if (IsValidUser(model.Email, model.HashPW))
            {
                //gets the first matching email then sets the user to the session so logs them in
                var user = context.Patrons.FirstOrDefault(u => u.Email == model.Email);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    HttpContext.Session.SetString("Name", user.Name.ToString());
                    HttpContext.Session.SetString("HashPW", user.HashPW.ToString());
                    return RedirectToAction("HomePage", "Home");
                }
                //does the same thing but if its an employee
                var IsEmployee = context.User.FirstOrDefault(u => u.Email == model.Email);

                if (IsEmployee != null)
                {
                    HttpContext.Session.SetString("UserId", IsEmployee.UserId.ToString());
                    HttpContext.Session.SetString("Name", IsEmployee.Name.ToString());
                    HttpContext.Session.SetString("HashPW", IsEmployee.HashPW.ToString());
                    //if admin sets the login user as admin.
                    if (IsEmployee.IsAdmin)
                    {
                        HttpContext.Session.SetString("IsAdmin", IsEmployee.IsAdmin.ToString());
                    }
                    return RedirectToAction("HomePage", "Home");

                }

            }
            else
            {
                //if it doesnt work show error
                //this line of code isnt mine https://stackoverflow.com/questions/5739362/modelstate-addmodelerror-how-can-i-add-an-error-that-isnt-for-a-property
                //is referenced in report
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                
            }
                
                //}
                
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //referenced code ends here 
            return View(model);
        }

        //checks to ensure the user is valid by taking their email and password, hashing the password with the associated salt, then comparing that password
        //to the databases hashed password
        private bool IsValidUser(string email, string password)
        {
            bool IsValid;
            //searches all users and gets the first one that matches the email of the model
            var patron = context.Patrons.SingleOrDefault(u => u.Email == email);
            //same thing for employees
            var user = context.User.SingleOrDefault(u => u.Email == email);

            if (patron != null)
            {


                //Combine the stored salt with the entered password
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
            else if (user != null)
            {
                //Combine the stored salt with the entered password
                string saltedPassword = password + user.Salt;

                string HashDB2 = user.HashPW;
                // hashes the password sent by the user then checks it the same
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, HashDB2);
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

        //initiates the login recovery process by showing the recovery page to start
        public IActionResult LoginRecovery(string email)
        {
            //checks to see if an email was inputted
            if (!string.IsNullOrEmpty(email))
            {
                //creates a context for the user
                var userList = context.User.ToList();
                var patronList = context.Patrons.ToList();
                //checks to see if the email entered in recovery matches a email in the database
                foreach (var patron in patronList)
                {
                    if (patron.Email.Equals(email))
                    {
                        //returns the page for simulating an email
                        return RedirectToAction("LoginRecoveryEmail", patron);
                    }
                }
            }
            //returns nothing if it does not satisfy above conditions
            return View();
        }
        //shows the simulated page for a login recovery email
        public ActionResult LoginRecoveryEmail(LoginViewModel Model)
        {
            return View(Model);
        }
        //changes the password of the user based on their input
        public ActionResult LoginRecoveryReset(LoginViewModel Model, String Password)
        {
            //ensures the password is not nothing
            if (!string.IsNullOrEmpty(Password))
            {
                //takes the massword, hashes it with the associated accounts salt, then ovverwrites the previous password
                Model.HashPW= Password;
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                //string saltedPassword = model.HashPW + salt;
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Model.HashPW, salt);
                var NewPatron = new Patrons
                {

                    HashPW = HashedPassword,
                    Email = Model.Email,
                    Name = Model.Name,
                    Salt = salt

                };
                context.Patrons.Update(NewPatron);
                context.SaveChanges();
                return RedirectToAction("LoginRecoverySuccess", "Login");

                
            }
            return View(Model);
        }
        //displays the page for a successful login recovery/password change
        public IActionResult LoginRecoverySuccess()
        { return View(); }

        //shows the account creation page
        public IActionResult CreateAccount()
        {

            return View("CreateAccountPage");
        }

        //performs the action of adding an account to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(LoginViewModel model)
            //added check to see if user already exists
        {
            
            if (ModelState.IsValid)
            {
                //generates a salt unique to the account
                //this is using code from https://jasonwatmore.com/post/2021/05/27/net-5-hash-and-verify-passwords-with-bcrypt 
                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                //string saltedPassword = model.HashPW + salt;
                //hashes the password based on the salt
                string HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.HashPW, salt);
                // code from the BCrypt script ends here

                //creates a new patron
                var NewPatron = new Patrons
                {
                   
                    HashPW = HashedPassword,
                    Email = model.Email,
                    Name = model.Name,
                    Salt = salt,
                    
                    
                };
                //adds the patron to the database
                context.Patrons.Add(NewPatron); 
                context.SaveChanges();
                return RedirectToAction("HomePage", "Home");
            }

            return View("CreateAccountPage", model);
        }
    }
}

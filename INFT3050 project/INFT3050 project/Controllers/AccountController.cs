using INFT3050_project.Models;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Mvc;

//this controller is used to edit the user and patron accounts
namespace INFT3050_project.Controllers
{
    public class AccountController : Controller
    {

        //calls the shopcontext class
        public ShopContext context;
        public AccountController(ShopContext ctx)
        {
            this.context = ctx;
        }

        //regular index. has no function
        public IActionResult Index()
        {
            return View();
        }
        //allows for editing of a patrons account information
        public IActionResult PatronAccountEdit()
        {
            //checks that a patrons id is passed and not empty
            if (HttpContext.Session.GetString("UserId") != null)
            {
                //creates a new view model for an account and calls the user id from the session
                var viewModel = new AccountViewModel();
                viewModel.UserId = HttpContext.Session.GetString("UserId");

                //looks for the patron in the database via the id supplied and then updates the information
                var patron = context.Patrons.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);
                if (patron != null)
                {
                    viewModel.Name = patron.Name;
                    viewModel.Email = patron.Email;
                }
                //returns the view with the updated viewmodel
                return View(viewModel);
            }
            //does nothing if no patronid
            else 
            {
                return View(); 
            }


        }
        //allows for editing of a users account information
        public IActionResult UserAccountEdit()
        {
            //checks to ensure a userid is passed to the system
            if (HttpContext.Session.GetString("UserId") != null)
            {
                //creates a new account view model and assigns the user id based on the session information it calls
                var viewModel = new AccountViewModel();
                viewModel.UserId = HttpContext.Session.GetString("UserId");

                //checks the database for a matching user and then updates the information provided
                var User = context.User.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);
                if (User != null)
                {
                    viewModel.Name = User.Name;
                    viewModel.Email = User.Email;
                }
                //returns the view with the updated view model
                return View(viewModel);
            }
            else { return View(); }


        }
        //returns page for patron account
        public IActionResult PatronAccount()
        {
            return View();
        }
        //returns page for user account
        public IActionResult UserAccount()
        {
            return View();
        }

        //gets the page for patron account edit
        [HttpGet]
        public IActionResult GoEdit()
        {
            return RedirectToAction("PatronAccountEdit");
        }
        [HttpGet]
        //gets the page for user account edit
        public IActionResult UserGoEdit()
        {
            return RedirectToAction("UserAccountEdit");
        }
        [HttpGet]
        //clears the session of the user and other information and returns them to homepage
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("HomePage", "Home");
        }
        // edts the patrons account information.
        [HttpPost]
        public IActionResult PatronAccountEdit(AccountViewModel model)
        {
            // creates a temporary view model based on the inputted view model
            var viewModel = model;
            //takes the sessions user id an applies it the the temp model
            viewModel.UserId = HttpContext.Session.GetString("UserId");

            // Retrieve the patron from the database using the UserId
            var patron = context.Patrons.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);

            if (patron != null)
            {
                //Check the current password
                if (model.CurrentPassword != null && BCrypt.Net.BCrypt.Verify(model.CurrentPassword, patron.HashPW))
                {
                    // Update the password to the new password
                    var newSalt = patron.Salt; // Keep the same salt
                    patron.HashPW = BCrypt.Net.BCrypt.HashPassword(model.NewPassword, newSalt);
                }

                // Update the Email
                if (!string.IsNullOrEmpty(model.Email))
                {
                    patron.Email = model.Email;
                    HttpContext.Session.SetString("Email", patron.Email);
                }
                //update the name
                if (!string.IsNullOrEmpty(model.Name))
                {
                    patron.Name = model.Name;
                    HttpContext.Session.SetString("Name", patron.Name);
                }
                //tries to make changes
                try
                {
                    context.Update(patron); // Save changes to the database
                    context.SaveChanges();
                }
                //if changes fail, throw an exception
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
                } // Save changes to the database
            }
            //return the updated model
            return View(viewModel);
        }
        //edits the account information of a user
        [HttpPost]
        public IActionResult UserAccountEdit(AccountViewModel model)
        {
            //creates a temporary view model and calls the user id from the session and assigns it to the temp model
            var viewModel = model;
            viewModel.UserId = HttpContext.Session.GetString("UserId");

            //retrieve the patron from the database using the UserId
            var User = context.User.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);

            if (User != null)
            {
                //check the current password
                if (model.CurrentPassword != null && BCrypt.Net.BCrypt.Verify(model.CurrentPassword, User.HashPW))
                {
                    //update the password to the new password
                    var newSalt = User.Salt; // Keep the same salt
                    User.HashPW = BCrypt.Net.BCrypt.HashPassword(model.NewPassword, newSalt);
                }

                //update the email
                if (!string.IsNullOrEmpty(model.Email))
                {
                    User.Email = model.Email;
                    HttpContext.Session.SetString("Email", User.Email);
                }
                //update the name
                if (!string.IsNullOrEmpty(model.Name))
                {
                    User.Name = model.Name;
                    HttpContext.Session.SetString("Name", User.Name);
                }
                //tries to update the database
                try
                {
                    context.Update(User); // Save changes to the database
                    context.SaveChanges();
                }
                //if it fails, throws an exception
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
                } // Save changes to the database
            }

            return View(viewModel);
        }




    }
}

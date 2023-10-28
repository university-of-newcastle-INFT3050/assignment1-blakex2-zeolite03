using INFT3050_project.Models;
using INFT3050_project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050_project.Controllers
{
    public class AccountController : Controller
    {


        public ShopContext context;
        public AccountController(ShopContext ctx)
        {
            this.context = ctx;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PatronAccountEdit()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                var viewModel = new AccountViewModel();
                viewModel.UserId = HttpContext.Session.GetString("UserId");

                var patron = context.Patrons.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);
                if (patron != null)
                {
                    viewModel.Name = patron.Name;
                    viewModel.Email = patron.Email;


                }


                return View(viewModel);
            }
            else { return View(); }


        }
        [HttpPost]
        public IActionResult PatronAccountEdit(AccountViewModel model)
        {
            var viewModel = model;
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

                // Update the Email and Name
                if (!string.IsNullOrEmpty(model.Email))
                {
                    patron.Email = model.Email;
                    HttpContext.Session.SetString("Email", patron.Email);
                }

                if (!string.IsNullOrEmpty(model.Name))
                {
                    patron.Name = model.Name;
                    HttpContext.Session.SetString("Name", patron.Name);
                }

                try
                {
                    context.Update(patron); // Save changes to the database
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log or handle the exception as needed
                    ModelState.AddModelError(string.Empty, "An error occurred while saving changes.");
                } // Save changes to the database
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UserAccountEdit(AccountViewModel model)
        {
            var viewModel = model;
            viewModel.UserId = HttpContext.Session.GetString("UserId");

            // Retrieve the patron from the database using the UserId
            var User = context.User.FirstOrDefault(u => u.UserId.ToString() == viewModel.UserId);

            if (User != null)
            {
                //Check the current password
                if (model.CurrentPassword != null && BCrypt.Net.BCrypt.Verify(model.CurrentPassword, User.HashPW))
                {
                    // Update the password to the new password
                    var newSalt = User.Salt; // Keep the same salt
                    User.HashPW = BCrypt.Net.BCrypt.HashPassword(model.NewPassword, newSalt);
                }

                // Update the Email and Name
                if (!string.IsNullOrEmpty(model.Email))
                {
                    User.Email = model.Email;
                    HttpContext.Session.SetString("Email", User.Email);
                }

                if (!string.IsNullOrEmpty(model.Name))
                {
                    User.Name = model.Name;
                    HttpContext.Session.SetString("Name", User.Name);
                }

                try
                {
                    context.Update(User); // Save changes to the database
                    context.SaveChanges();
                }
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

using Microsoft.AspNetCore.Mvc;

namespace INFT3050_project.Controllers
{
    public class TestRouteController : Controller
    {
        public IActionResult HomePage()
        {
            return View("Test");
        }

        public IActionResult Test(string id = "All")
        {
            ViewBag.Product = id;
            return View();
        }
    }
}

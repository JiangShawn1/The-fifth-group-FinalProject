using Microsoft.AspNetCore.Mvc;

namespace The_fifth_group_FinalProject.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult commodity(int id)
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}

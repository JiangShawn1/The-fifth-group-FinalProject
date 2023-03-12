using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace The_fifth_group_FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        // GET: OrdersController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
    }
}

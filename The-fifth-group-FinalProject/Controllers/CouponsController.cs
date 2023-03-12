using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace The_fifth_group_FinalProject.Controllers
{
    public class CouponsController : Controller
    {
        // GET: CouponsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CouponsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
    }
}

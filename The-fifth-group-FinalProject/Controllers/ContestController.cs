using Microsoft.AspNetCore.Mvc;

namespace The_fifth_group_FinalProject.Controllers
{
	public class ContestController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ContestIndex()
		{
			return View();
		}

		public IActionResult ContestDetail(int id)
		{
			return View();
		}
	}
}

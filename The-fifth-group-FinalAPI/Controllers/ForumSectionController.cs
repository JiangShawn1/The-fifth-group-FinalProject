using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using The_fifth_group_FinalAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace The_fifth_group_FinalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ForumSectionController : ControllerBase
	{
		private readonly TheFifthGroupOfTopicsContext _theFifthGroupOfTopicsContext;


		public ForumSectionController(TheFifthGroupOfTopicsContext theFifthGroupOfTopicsContext)
		{
			_theFifthGroupOfTopicsContext = theFifthGroupOfTopicsContext;
		}



		// GET: api/<ForumSectionController>
		[HttpGet]
		public ActionResult<IEnumerable<ForumSection>> Get()
		{
			return _theFifthGroupOfTopicsContext.ForumSection.ToList();
		}

		// GET api/<ForumSectionController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return ;
		}

		// POST api/<ForumSectionController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<ForumSectionController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<ForumSectionController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

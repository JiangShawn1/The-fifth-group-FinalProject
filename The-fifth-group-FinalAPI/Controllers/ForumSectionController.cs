using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using The_fifth_group_FinalAPI.Models;
using System.Linq;
using The_fifth_group_FinalAPI.DTOs;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace The_fifth_group_FinalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ForumSectionController : ControllerBase
	{
		private readonly TheFifthGroupOfTopicsContext _context;


		public ForumSectionController(TheFifthGroupOfTopicsContext theFifthGroupOfTopicsContext)
		{
			_context = theFifthGroupOfTopicsContext;
		}

		 //GET: api/<ForumSectionController>
		[HttpGet]
		//public async Task<IEnumerable<Forum_FirstDTO>> GetFirstForum()
		//{
			
		//var forum_first = _context.ForumSection.Include(a=> a.ForumSectionBranches)
		//		.Select(k=> new Forum_FirstDTO
		//				{
		//				Id = k.Id,
		//				SectionName = k.SectionName,
		//				SectionNameId = k.Id,
		//				BranchName = k.BranchName,
		//				AdministratorId = k.MemberId
		//				}).ToListAsync();



			//from a in _context.ForumSection
			//join b in _context.ForumSectionBranches on _context.Forum.SectionNameId equals a.id
			//join c in _context.Members on b.AdministratorId equals _c.MemberId
			//select new Forum_FirstDTO

			// return await forum_first;
			
		//}

		// GET api/<ForumSectionController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return ""+id ;
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

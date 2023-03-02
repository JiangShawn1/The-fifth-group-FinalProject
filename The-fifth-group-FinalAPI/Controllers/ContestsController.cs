using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
    [ApiController]
    public class ContestsController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ContestsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Contests
        [HttpGet]
        public async Task<IEnumerable<ContestsDTO>> GetContests()
        {
            var contest = _context.Contests.Where(c=>c.Review==true);

			return contest.Select(con=> new ContestsDTO
			{
				Id = con.Id,
				Name = con.Name,
				Area = con.Area,
				ContestDate = con.ContestDate,
				Distance = con.ContestCategory.Where(c => c.ContestId == con.Id).Select(c => c.Category.Distance).OrderBy(x=>x).ToList(),
				RegistrationDeadline = con.RegistrationDeadline,
			});		
		}

		[HttpPost("Filter")]
		public async Task<IEnumerable<ContestsDTO>> FilterContests([FromBody] ContestsDTO contestDTO)
		{
			var contest = _context.Contests.Where(c => c.Review == true);
			return contest.Where(
				con => con.Name.Contains(contestDTO.Name!) || con.Area.Contains(contestDTO.Name!)).Select(con => new ContestsDTO
				{
					Id = con.Id,
					Name = con.Name,
					Area = con.Area,
					ContestDate = con.ContestDate,
					Distance = con.ContestCategory.Where(c => c.ContestId == con.Id).Select(c => c.Category.Distance).OrderBy(x => x).ToList(),
					RegistrationDeadline = con.RegistrationDeadline,

				});
		}

		// GET: api/Contests/5
		[HttpGet("{id}")]
        public async Task<ContestsDTO> GetContests(int id)
        {		
			var contestCategory = _context.ContestCategory.Include("Category").Include("Contest").Where(c=>c.ContestId==id);
            var supplier = _context.Suppliers.Where(c=>c.SupplierId== contestCategory.First().Contest.SupplierId);
            var CategoryName = contestCategory.Select(c => c.Category.Category).ToList();
			var Distance = contestCategory.Select(c => c.Category.Distance).ToList();
            var Quota = contestCategory.Select(c => c.Quota).ToList();
            var EnterFee = contestCategory.Select(c => c.EnterFee).ToList();
            List<CategoryGroup> categoryGroupList= new List<CategoryGroup>();
            for (int i = 0; i < CategoryName.Count; i++)
            {
                CategoryGroup categoryGroup = new CategoryGroup
                { 
                    CategoryName = CategoryName[i] ,
                    Distance= Distance[i],
                    Quota = Quota[i],
                    EnterFee= EnterFee[i],
                };
				categoryGroupList.Add(categoryGroup);
			};
            if (contestCategory == null)
            {
                return null;
            }
			ContestsDTO ConDTO = new ContestsDTO
            {
                Id= id,
				Name = contestCategory.First().Contest.Name,
				ContestDate = contestCategory.First().Contest.ContestDate,
                CreateDateTime= contestCategory.First().Contest.CreateDateTime,
				RegistrationDeadline = contestCategory.First().Contest.RegistrationDeadline,
				Area = contestCategory.First().Contest.Area,
                Location = contestCategory.First().Contest.Location,
                MapUrl = contestCategory.First().Contest.MapUrl,
                Detail= contestCategory.First().Contest.Detail,
                SupplierName= supplier.First().SupplierName,         
                CategoryGroups= categoryGroupList.OrderBy(x=>x.Distance).ToList(),
			};         
			return ConDTO;
		}

        // PUT: api/Contests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContests(int id, Contests contests)
        {
            if (id != contests.Id)
            {
                return BadRequest();
            }

            _context.Entry(contests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContestsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contests>> PostContests(Contests contests)
        {
            _context.Contests.Add(contests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContests", new { id = contests.Id }, contests);
        }

        // DELETE: api/Contests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContests(int id)
        {
            var contests = await _context.Contests.FindAsync(id);
            if (contests == null)
            {
                return NotFound();
            }

            _context.Contests.Remove(contests);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContestsExists(int id)
        {
            return _context.Contests.Any(e => e.Id == id);
        }
    }
}

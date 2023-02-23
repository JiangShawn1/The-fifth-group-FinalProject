using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
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
            return _context.Contests.Select(con => new ContestsDTO
			{
                Id= con.Id,
				Name = con.Name,
				Area = con.Area,
				ContestDate = con.ContestDate,
				Distance = con.ContestCategory.Where(c => c.ContestId == con.Id).Select(c => c.Category.Distance).ToList(),
                RegistrationDeadline = con.RegistrationDeadline,
			});
		}

        // GET: api/Contests/5
        [HttpGet("{id}")]
        public async Task<ContestsDTO> GetContests(int id)
        {
            var contestCategory = _context.ContestCategory.Include("Category").Include("Contest").Where(c=>c.ContestId==id);
            var supplier = _context.Suppliers.Where(c=>c.SupplierId== contestCategory.First().Contest.SupplierId);


			if (contestCategory == null)
            {
                return null;
            }
			ContestsDTO ConDTO = new ContestsDTO
            {
                Id= id,
				Name = contestCategory.First().Contest.Name,
				ContestDate = contestCategory.First().Contest.ContestDate,
				RegistrationDeadline = contestCategory.First().Contest.RegistrationDeadline,
				Area = contestCategory.First().Contest.Area,
                Location = contestCategory.First().Contest.Location,
                MapUrl = contestCategory.First().Contest.MapUrl,
                Detail= contestCategory.First().Contest.Detail,
                SupplierName= supplier.First().SupplierName,
                CategoryName = contestCategory.Select(c => c.Category.Category).ToList(),
                Quota = contestCategory.Select(c => c.Quota).ToList(),
                EnterFee = contestCategory.Select(c => c.EnterFee).ToList(),
                Distance =contestCategory.Select(c=> c.Category.Distance).ToList(),
               
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

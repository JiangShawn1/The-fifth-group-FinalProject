using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public InformationController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Information>>> GetInformation()
        {
            return await _context.Information.ToListAsync();
        }

        // GET: api/Information/5
        [HttpGet("{id}")]
        public async Task<InformationDTO> GetInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);

            if (information == null)
            {
                return null;
            }
			InformationDTO InfDTO = new InformationDTO
			{
				Id = information.Id,
				Name = information.Name,
				Phone = information.Phone,
				Gender = information.Gender,
                Address= information.Address,
			};
			return InfDTO;
			
        }
		

		// PUT: api/Information/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutInformation(int id, Information information)
        {
            if (id != information.Id)
            {
                return BadRequest();
            }

            _context.Entry(information).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformationExists(id))
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

        // POST: api/Information
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Information>> PostInformation(Information information)
        {
            _context.Information.Add(information);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInformation", new { id = information.Id }, information);
        }

        // DELETE: api/Information/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInformation(int id)
        {
            var information = await _context.Information.FindAsync(id);
            if (information == null)
            {
                return NotFound();
            }

            _context.Information.Remove(information);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InformationExists(int id)
        {
            return _context.Information.Any(e => e.Id == id);
        }
    }
}

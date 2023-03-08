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
    public class RegistrationInformationsController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public RegistrationInformationsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/RegistrationInformations
        [HttpGet]
        public async Task<IEnumerable<RegistrationInformationsDTO>> GetRegistrationInformation()
        {
            var memberid = 1;
            var member=_context.Members.Where(x=>x.MemberId== memberid);
            var registration = _context.RegistrationInformation
                .Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Category)
				.Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Contest)			
				.Include(r => r.Information)
                .Where(x => x.Registration.MemberId==memberid);

			return registration.Select(reg => new RegistrationInformationsDTO
			{
				Id = reg.Id,
                RegistrationId=reg.RegistrationId,
                InformationId=reg.InformationId,
                ContestCategoryId=reg.Registration.ContestCategoryId,
                ContestId=reg.Registration.ContestCategory.Contest.Id,
                ContestDate=reg.Registration.ContestCategory.Contest.ContestDate,
				ContestName = reg.Registration.ContestCategory.Contest.Name,
				CategoryName = reg.Registration.ContestCategory.Category.Category,
                Member=member.First().Name,
                MemberId=memberid,
                Name=reg.Information.Name,
                PaymentStatus=reg.Registration.PaymentStatus,
			});
		}
		// POST: api/RegistrationInformations/Filter
		[HttpPost("Filter")]
		public async Task<IEnumerable<RegistrationInformationsDTO>> FilterRegistrationInformation([FromBody] RegistrationInformationsDTO registrationDTO)
		{
			var memberid = 1;
			var member = _context.Members.Where(x => x.MemberId == memberid);
			var registration = _context.RegistrationInformation
				.Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Category)
				.Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Contest)
				.Include(r => r.Information)
				.Where(x => x.Registration.MemberId == memberid);
			return registration.Where(reg =>                
            reg.Registration.ContestCategory.Contest.Name.Contains(registrationDTO.Name!) ||
            reg.Information.Name.Contains(registrationDTO.Name!))
                .Select(reg => new RegistrationInformationsDTO
				{
					Id = reg.Id,
					RegistrationId = reg.RegistrationId,
					InformationId = reg.InformationId,
					ContestCategoryId = reg.Registration.ContestCategoryId,
					ContestId = reg.Registration.ContestCategory.Contest.Id,
					ContestDate = reg.Registration.ContestCategory.Contest.ContestDate,
					ContestName = reg.Registration.ContestCategory.Contest.Name,
					CategoryName = reg.Registration.ContestCategory.Category.Category,
					Member = member.First().Name,
					MemberId = memberid,
					Name = reg.Information.Name,
					PaymentStatus = reg.Registration.PaymentStatus,
				});
		}

		// GET: api/RegistrationInformations/5
		[HttpGet("{id}")]
        public async Task<ActionResult<RegistrationInformation>> GetRegistrationInformation(int memberid, int id)
        {
            var registrationInformation = await _context.RegistrationInformation.FindAsync(id);

            if (registrationInformation == null)
            {
                return NotFound();
            }

            return registrationInformation;
        }

        // PUT: api/RegistrationInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistrationInformation(int id, RegistrationInformation registrationInformation)
        {
            if (id != registrationInformation.Id)
            {
                return BadRequest();
            }

            _context.Entry(registrationInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationInformationExists(id))
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

        // POST: api/RegistrationInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
		//public async Task<ActionResult<RegistrationInformation>> PostRegistrationInformation(RegistrationInformation registrationInformation)
		//{
		//    _context.RegistrationInformation.Add(registrationInformation);
		//    await _context.SaveChangesAsync();

		//    return CreatedAtAction("GetRegistrationInformation", new { id = registrationInformation.Id }, registrationInformation);
		//}
		[HttpPost]
		public async Task<string> PostRegistrationInformation(RegistrationInformationsDTO RegInfDTO)
		{
			Information information = new Information
			{
				Name = RegInfDTO.Name,
				Phone = RegInfDTO.Phone,
				Gender = Convert.ToBoolean(RegInfDTO.Gender),
				Address = RegInfDTO.Address,
			};			 
			_context.Information.Add(information);

            var contestCategoryId =_context.ContestCategory.Where(c=>
            c.ContestId==RegInfDTO.ContestId&&
            c.CategoryId == RegInfDTO.CategoryId).Select(c=>c.Id).First();

            Registration registration = new Registration
            {
                MemberId= RegInfDTO.MemberId,
				ContestCategoryId= contestCategoryId,               
			};
			_context.Registration.Add(registration);
			await _context.SaveChangesAsync();

			RegistrationInformation registrationInformation =new RegistrationInformation
			{
				InformationId= information.Id,
                RegistrationId= registration.Id,
			};
			_context.RegistrationInformation.Add(registrationInformation);

			await _context.SaveChangesAsync();
			return "報名成功!";
		}
		// DELETE: api/RegistrationInformations/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrationInformation(int id)
        {
            var registrationInformation = await _context.RegistrationInformation.FindAsync(id);
            if (registrationInformation == null)
            {
                return NotFound();
            }

            _context.RegistrationInformation.Remove(registrationInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrationInformationExists(int id)
        {
            return _context.RegistrationInformation.Any(e => e.Id == id);
        }
    }
}

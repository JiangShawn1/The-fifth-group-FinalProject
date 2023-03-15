using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public class OrdersController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public OrdersController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetAllOrders(int memberId)
        {
            return await _context.Orders.Where(o => o.MemberId == memberId).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        [HttpGet("PayInfo/{id}")]
        public async Task<PayInfoDto> GetPayInfo(int id)
        {
            var reg = _context.RegistrationInformation
                .Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Category)
                .Include(r => r.Registration).ThenInclude(r => r.ContestCategory).ThenInclude(r => r.Contest)
                .Include(r => r.Information)
                .FirstOrDefault(x => x.Id == id);

            if (reg == null)
            {
                return null;
            }

            PayInfoDto payInfoDto = new PayInfoDto()
            {
                Id = reg.Id,
                Name = reg.Information.Name,
                Category = reg.Registration.ContestCategory.Category.Category,
                Contests = reg.Registration.ContestCategory.Contest.Name,
                Distance = reg.Registration.ContestCategory.Category.Distance,
                EnterFee = reg.Registration.ContestCategory.EnterFee,
                RegistrationId = reg.Registration.ContestCategory.Id,
                //Id = 1,
                //Name = "安安",
                //Category = "普通組",
                //Contests = "超跑大賽",
                //Distance = 42,
                //EnterFee = 1000,
                //RegistrationId = 1,
            };

            return payInfoDto;

        }
        [HttpPut("Paided/{id}")]
        public async Task<IActionResult> Paided(int id)
        {
            Registration reg = await _context.Registration.FindAsync(id);
            if (reg == null)
            {
                return BadRequest();
            }
            reg.PaymentStatus = true;

            _context.Entry(reg).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, Orders orders)
        {
            if (id != orders.Id)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.Id }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Filter")]    //Uri: api/Orders/Filter
        public async Task<IEnumerable<Orders>> FilterOrders([FromBody] Orders orders)
        {
            return _context.Orders
                .Where(o => o.OrderNumber.Contains(orders.OrderNumber));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}

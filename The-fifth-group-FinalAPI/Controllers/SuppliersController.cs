using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class SuppliersController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public SuppliersController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Suppliers>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suppliers>> GetSuppliers(int id)
        {
            var suppliers = await _context.Suppliers.FindAsync(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            return suppliers;
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuppliers(int id, Suppliers suppliers)
        {
            if (id != suppliers.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(suppliers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       
        [HttpPost]
        [Route("SupplierLogin")]
        [AllowAnonymous]
        public async Task<SuppliersDTO> SupplierLogin(SuppliersDTO supplier)
        {
            var result = GetByAccount(supplier.SupplierAdd, supplier.SupplierTel);

            if (result == null)
            {
                return null;
            }

            return result;
        }
        SuppliersDTO GetByAccount(string supplierAdd, string supplierTel)
        {
            var supplier = _context.Suppliers.SingleOrDefault(x => x.SupplierAdd == supplierAdd);

            if (supplier == null) { return null; }
            if (supplier.SupplierTel != supplierTel) { return null; }
            return new SuppliersDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierAdd = supplier.SupplierAdd,
                SupplierTel = supplier.SupplierTel,
            };
        }
        //public async Task<ActionResult<Suppliers>> PostSuppliers(Suppliers suppliers)
        //{
        //    _context.Suppliers.Add(suppliers);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSuppliers", new { id = suppliers.SupplierId }, suppliers);
        //}

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuppliers(int id)
        {
            var suppliers = await _context.Suppliers.FindAsync(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(suppliers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SuppliersExists(int id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}

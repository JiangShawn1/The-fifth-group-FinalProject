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
    public class ProductsController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ProductsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> SearchProducts(string? brand, string? color, string? keyword)
        {
            IEnumerable<Products> products = _context.Products.Include(p => p.Brand).Include(p => p.Color);

            if (!string.IsNullOrEmpty(keyword)) products = products.Where(x => x.ProductName.Contains(keyword));

            if (!string.IsNullOrEmpty(brand)) products = products.Where(x => x.Brand.Brand.Contains(brand));

            if (!string.IsNullOrEmpty(color)) products = products.Where(x => x.Color.Color.Contains(color));


            return Ok(products.Select(x => new ProductsDTO()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Brand = x.Brand.Brand,
                Color = x.Color.Color,
                ImageUrl= x.ImageUrl,

            }));

        }




        // GET: api/Products
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ProductsDTO>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Color)
                .Select(p => new ProductsDTO
                {
                    Id = p.Id,
                    ImageUrl = p.ImageUrl,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Brand = p.Brand.Brand,
                    Color = p.Color.Color,
                })
                .ToListAsync();

            return products;
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
	[EnableCors("AllowAny")]
	public class Products1Controller : Controller
	{
		private readonly TheFifthGroupOfTopicsContext _context;

		public Products1Controller(TheFifthGroupOfTopicsContext context)
		{
			_context = context;
		}


		// GET: api/Products
		[HttpGet]
		public async Task<IEnumerable<ProductsDTO>> GetProducts()
		{
			return _context.Products.Select(p => new ProductsDTO
			{
				Id = p.Id,
				Brand_Id = p.BrandId,
				ProductName = p.ProductName,
				Color_Id = p.ColorId,
				Price = p.Price,
			}).OrderBy(p => p.Brand_Id);
		}

		// GET: api/Products/5
		[HttpGet("{Id}")]
		public async Task<IEnumerable<ProductsDTO>> GetProducts(int Id)
		{
			var Products = _context.Products.Where(p => p.Id == Id);
			return Products.Include(p => p.Brand).Include(p => p.Color).Select(p => new ProductsDTO
			{
				Id = p.Id,
				Brand = p.Brand.Brand,
				ProductName = p.ProductName,
				Color = p.Color.Color,
				Price = p.Price,
				ProductIntroduce = p.ProductIntroduce,
				ImageUrl = p.ImageUrl,
			}).OrderBy(p => p.Id);
		}




		//// PUT: api/Products/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutCartItems(int id, CartItems cartItems)
		//{
		//	if (id != cartItems.Id)
		//	{
		//		return BadRequest();
		//	}

		//	_context.Entry(cartItems).State = EntityState.Modified;

		//	try
		//	{
		//		await _context.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!ProductsExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		// GET: Products1
		public async Task<IActionResult> Index()
		{
			var theFifthGroupOfTopicsContext = _context.Products.Include(p => p.Brand).Include(p => p.Color);
			return View(await theFifthGroupOfTopicsContext.ToListAsync());
		}

		// GET: Products1/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var products = await _context.Products
				.Include(p => p.Brand)
				.Include(p => p.Color)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (products == null)
			{
				return NotFound();
			}

			return View(products);
		}

		// GET: Products1/Create
		public IActionResult Create()
		{
			ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand");
			ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Color");
			return View();
		}

		// POST: Products1/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,BrandId,ProductName,ProductIntroduce,ColorId,Price,ImageUrl")] Products products)
		{
			if (ModelState.IsValid)
			{
				_context.Add(products);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand", products.BrandId);
			ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Color", products.ColorId);
			return View(products);
		}

		// GET: Products1/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var products = await _context.Products.FindAsync(id);
			if (products == null)
			{
				return NotFound();
			}
			ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand", products.BrandId);
			ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Color", products.ColorId);
			return View(products);
		}

		// POST: Products1/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,BrandId,ProductName,ProductIntroduce,ColorId,Price,ImageUrl")] Products products)
		{
			if (id != products.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(products);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductsExists(products.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Brand", products.BrandId);
			ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Color", products.ColorId);
			return View(products);
		}

		// GET: Products1/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var products = await _context.Products
				.Include(p => p.Brand)
				.Include(p => p.Color)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (products == null)
			{
				return NotFound();
			}

			return View(products);
		}

		// POST: Products1/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'TheFifthGroupOfTopicsContext.Products'  is null.");
			}
			var products = await _context.Products.FindAsync(id);
			if (products != null)
			{
				_context.Products.Remove(products);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductsExists(int id)
		{
			return _context.Products.Any(e => e.Id == id);
		}
	}
}

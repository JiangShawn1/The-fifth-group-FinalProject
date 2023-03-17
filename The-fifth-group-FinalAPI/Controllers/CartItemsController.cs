using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
	[EnableCors("AllowAny")]
	[Route("api/[controller]")]
	[ApiController]
	public class CartItemsController : ControllerBase
	{

		private readonly TheFifthGroupOfTopicsContext _context;

		public CartItemsController(TheFifthGroupOfTopicsContext context)
		{
			_context = context;
		}




		[HttpGet("{memberId}")]
		public async Task<ActionResult<List<CartItemsDTO>>> GetCartItemsByMemberId(int memberId)
		{
			var cartItems = await _context.CartItems
				.Include(c => c.Product)
				.Where(c => c.MemberId == memberId)
				.ToListAsync();

			if (cartItems == null)
			{
				return NotFound();
			}

			var cartItemsDTOs = new List<CartItemsDTO>();
			foreach (var cartItem in cartItems)
			{
				cartItemsDTOs.Add(new CartItemsDTO()
				{
					Id = cartItem.Id,
					ImageUrl = cartItem.Product.ImageUrl,
					Member_Id = cartItem.MemberId,
					ProductName = cartItem.Product.ProductName,
					Price = cartItem.Product.Price,
					Qty = cartItem.Qty

				});
			}

			return cartItemsDTOs;

			//return new List<CartItemsDTO>()
			//{
			//	new CartItemsDTO()
			//	{
   //                 ProductName = "波奇塔",
   //                 ImageUrl = "https://i.imgur.com/sSqc1ZN.gif",
   //                 Qty = 10,
   //                 Price = 100,
   //             },

   //             new CartItemsDTO()
   //             {
   //                 ProductName = "小小紅",
   //                 ImageUrl = "https://i.imgur.com/sCRL4fF.gif",
   //                 Qty = 5,
   //                 Price = 50,
   //             }
   //         };

        }


		//完成
		[HttpPost]
		public async Task<IActionResult> AddCartItem(int memberId, int productId, int qty)
		{
			// 檢查商品是否存在
			var product = await _context.Products.FindAsync(productId);
			if (product == null)
			{
				return NotFound("Product not found");
			}

			// 檢查會員是否存在
			var member = await _context.Members.FindAsync(memberId);
			if (member == null)
			{
				return NotFound("Member not found");
			}

			// 新增資料到CartItems資料表中
			var cartItem = new CartItems
			{
				MemberId = memberId,
				ProductId = productId,
				Qty = qty
			};
			_context.CartItems.Add(cartItem);
			await _context.SaveChangesAsync();

			return Ok("CartItem added successfully");
		}

		//[Route("api/[controller]/[action]")]
		//[HttpPost]
		//public async Task<IActionResult> AddCartItem(CartItemsDTO cartItemDTO)
		//{
		//	var cartItem = new CartItems
		//	{
		//		MemberId = cartItemDTO.Member_Id,
		//		ProductId = cartItemDTO.Product_Id,
		//		Qty = cartItemDTO.Qty
		//	};

		//	_context.CartItems.Add(cartItem);
		//	await _context.SaveChangesAsync();

		//	return CreatedAtAction("GetCartItem", new { id = cartItem.Id }, cartItem);
		//}

		//完成
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCartItem(int id)
		{
			var cartItem = await _context.CartItems.FindAsync(id);

			if (cartItem == null)
			{
				return NotFound();
			}

			_context.CartItems.Remove(cartItem);
			await _context.SaveChangesAsync();

			return NoContent();
		}
		//完成
		[HttpPut("cartitems/{id}/quantity")]
		public async Task<IActionResult> UpdateCartItemQuantity(int id, [FromBody] int quantity)
		{
			var cartItem = await _context.CartItems.FindAsync(id);

			if (cartItem == null)
			{
				return NotFound();
			}

			cartItem.Qty = quantity;

			if (quantity == 0)
			{
				_context.CartItems.Remove(cartItem);
			}
			else
			{
				_context.CartItems.Update(cartItem);
			}

			await _context.SaveChangesAsync();

			return NoContent();
		}

		//[HttpGet("{id}")]
		//public async Task<ActionResult<CartItemsDTO>> GetCartItemsByMemberId(int id)
		//{
		//	var cartItems = await _context.CartItems
		//		.Include(c => c.Product)
		//		.Include(c => c.Member)
		//		.FirstOrDefaultAsync(p => p.Id == id);

		//	if (cartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	return new CartItemsDTO()
		//	{
		//		Member_Id = cartItems.MemberId,
		//		ProductName = cartItems.Product.ProductName,
		//		Qty = cartItems.Qty

		//	};
		//}




		//[HttpPut("{id}")]
		//public async Task<IActionResult> UpdateCartItem(int id, CartItemsDTO cartItemDto)
		//{
		//    if (id != cartItemDto.Id)
		//    {
		//        return BadRequest();
		//    }

		//    var cartItem = await _context.CartItems.FindAsync(id);

		//    if (cartItem == null)
		//    {
		//        return NotFound();
		//    }

		//    if (cartItemDto.Qty == 0)
		//    {
		//        _context.CartItems.Remove(cartItem);
		//    }
		//    else
		//    {
		//        cartItem.Qty = cartItemDto.Qty;
		//        _context.Entry(cartItem).State = EntityState.Modified;
		//    }

		//    try
		//    {
		//        await _context.SaveChangesAsync();
		//    }
		//    catch (DbUpdateConcurrencyException)
		//    {
		//        if (!CartItemsExists(id))
		//        {
		//            return NotFound();
		//        }
		//        else
		//        {
		//            throw;
		//        }
		//    }

		//    return NoContent();
		//}


		// GET: CartItems
		//public async Task<IActionResult> Index()
		//{
		//	var theFifthGroupOfTopicsContext = _context.CartItems.Include(c => c.Member).Include(c => c.Product);
		//	return View(await theFifthGroupOfTopicsContext.ToListAsync());
		//}

		//// GET: CartItems/Details/5
		//public async Task<IActionResult> Details(int? id)
		//{
		//	if (id == null || _context.CartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	var cartItems = await _context.CartItems
		//		.Include(c => c.Member)
		//		.Include(c => c.Product)
		//		.FirstOrDefaultAsync(m => m.Id == id);
		//	if (cartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(cartItems);
		//}

		//// GET: CartItems/Create
		//public IActionResult Create()
		//{
		//	ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account");
		//	ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ImageUrl");
		//	return View();
		//}

		//// POST: CartItems/Create
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Create([Bind("Id,MemberId,ProductId,Qty")] CartItems cartItems)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		_context.Add(cartItems);
		//		await _context.SaveChangesAsync();
		//		return RedirectToAction(nameof(Index));
		//	}
		//	ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cartItems.MemberId);
		//	ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ImageUrl", cartItems.ProductId);
		//	return View(cartItems);
		//}

		//// GET: CartItems/Edit/5
		//public async Task<IActionResult> Edit(int? id)
		//{
		//	if (id == null || _context.CartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	var cartItems = await _context.CartItems.FindAsync(id);
		//	if (cartItems == null)
		//	{
		//		return NotFound();
		//	}
		//	ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cartItems.MemberId);
		//	ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ImageUrl", cartItems.ProductId);
		//	return View(cartItems);
		//}

		//// POST: CartItems/Edit/5
		//// To protect from overposting attacks, enable the specific properties you want to bind to.
		//// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(int id, [Bind("Id,MemberId,ProductId,Qty")] CartItems cartItems)
		//{
		//	if (id != cartItems.Id)
		//	{
		//		return NotFound();
		//	}

		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{
		//			_context.Update(cartItems);
		//			await _context.SaveChangesAsync();
		//		}
		//		catch (DbUpdateConcurrencyException)
		//		{
		//			if (!CartItemsExists(cartItems.Id))
		//			{
		//				return NotFound();
		//			}
		//			else
		//			{
		//				throw;
		//			}
		//		}
		//		return RedirectToAction(nameof(Index));
		//	}
		//	ViewData["MemberId"] = new SelectList(_context.Members, "MemberId", "Account", cartItems.MemberId);
		//	ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ImageUrl", cartItems.ProductId);
		//	return View(cartItems);
		//}

		//// GET: CartItems/Delete/5
		//public async Task<IActionResult> Delete(int? id)
		//{
		//	if (id == null || _context.CartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	var cartItems = await _context.CartItems
		//		.Include(c => c.Member)
		//		.Include(c => c.Product)
		//		.FirstOrDefaultAsync(m => m.Id == id);
		//	if (cartItems == null)
		//	{
		//		return NotFound();
		//	}

		//	return View(cartItems);
		//}

		//// POST: CartItems/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	if (_context.CartItems == null)
		//	{
		//		return Problem("Entity set 'TheFifthGroupOfTopicsContext.CartItems'  is null.");
		//	}
		//	var cartItems = await _context.CartItems.FindAsync(id);
		//	if (cartItems != null)
		//	{
		//		_context.CartItems.Remove(cartItems);
		//	}

		//	await _context.SaveChangesAsync();
		//	return RedirectToAction(nameof(Index));
		//}

		private bool CartItemsExists(int id)
		{
			return _context.CartItems.Any(e => e.Id == id);
		}
	}
}

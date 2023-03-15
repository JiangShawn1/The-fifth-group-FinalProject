using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Xml.Linq;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalProject.Helpers;
using The_fifth_group_FinalProject.Models;

namespace The_fifth_group_FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        // GET: OrdersController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }
        public async Task<ActionResult> Products(int id)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://localhost:7040/api/Orders/PayInfo/{id}");
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<PayInfoDto>(responseContent);

                    if (data == null)
                    {
                        return View("Error");
                    }

                    return View(data);
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Error");
            }
        }

        // GET: OrdersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //public IActionResult Checkout()
        //{
        //    if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
        //    {
        //        return RedirectToAction("Index", "Cart");
        //    }

        //    var myOrder = new Order()
        //    {
        //        MemberId = 0,     //取得訂購人ID
        //        OrderNumber = "0", //取得訂購人帳號
        //        OrderItem = SessionHelper.                 //取得購物車資料
        //            GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart")
        //    };
        //    myOrder.Amount = myOrder.OrderItem.Sum(m => m.SubTotal); //計算訂單總額

        //    ViewBag.CartItems = SessionHelper.
        //        GetObjectFromJson<List<PCartItem>>(HttpContext.Session, "cart");

        //    return View(myOrder);
        //}

        //// 新增訂單到資料庫
        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        order.CreateAt = DateTime.Now;    //取得當前時間
        //        order.TradeStatus = 0;              //付款狀態預設為False
        //        order.OrderItem = SessionHelper.   //綁定訂單內容
        //            GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");

        //        //_context.Add(order);               //將訂單寫入資料庫
        //        //await _context.SaveChangesAsync();
        //        SessionHelper.Remove(HttpContext.Session, "cart");

        //        //完成後畫面移轉至ReviewOrder()
        //        return RedirectToAction("ReviewOrder", new { Id = order.Id });
        //    }
        //    return View("Checkout");
        //}
        //public async Task<IActionResult> ReviewOrder(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        return NotFound();
        //    }
        //    //從資料庫取得訂單資料
        //    var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == Id);
        //    //if (order.UserId != _userManager.GetUserId(User))
        //    //{
        //    //	return NotFound();
        //    //}
        //    //else
        //    //{
        //    //取得訂單內容
        //    order.OrderItem = await _context.OrderItem
        //        .Where(p => p.OrderId == Id).ToListAsync();
        //    ViewBag.orderItems = GetOrderItems(order.Id);
        //    //}
        //    return View(order);
        //}

        //// 付款
        //public async Task<IActionResult> Payment(int? Id, bool isSuccess)
        //{
        //    if (Id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order.FirstOrDefaultAsync(p => p.Id == Id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (isSuccess)
        //        {
        //            order.isPaid = true;
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();  //更新訂單狀態
        //        }
        //        return RedirectToAction("ReviewOrder", new { Id = order.Id });
        //    }
        //}
        //private List<CartItem> GetOrderItems(int orderId)
        //{
        //    var OrderItems = _context.OrderItem.Where(p => p.OrderId == orderId).ToList();
        //    List<CartItem> orderItems = new List<CartItem>();
        //    foreach (var orderitem in OrderItems)
        //    {
        //        CartItem item = new PCartItem(orderitem);
        //        item.Product = _context.Product.Single(x => x.Id == orderitem.ProductId);
        //        orderItems.Add(item);
        //    }
        //    return orderItems;
        //}
    }
}

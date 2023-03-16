using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;
using The_fifth_group_FinalProject.Helpers;
using The_fifth_group_FinalProject.Models;
using static NuGet.Packaging.PackagingConstants;

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

        public IActionResult Checkout()
        {
            //if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            //{
            //    return RedirectToAction("Index", "Cart");
            //}

            //var myOrder = new Order()
            //{
            //    MemberId = 0,     //取得訂購人ID
            //    OrderNumber = "0", //取得訂購人帳號
            //    OrderItem = SessionHelper.                 //取得購物車資料
            //        GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart")
            //};
            //myOrder.Amount = myOrder.OrderItem.Sum(m => m.SubTotal); //計算訂單總額

            //ViewBag.CartItems = SessionHelper.
            //    GetObjectFromJson<List<PCartItem>>(HttpContext.Session, "cart");

            //return View(myOrder);
            return View();
        }

        // 新增訂單到資料庫
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://localhost:7040/api/CartItems/{order.MemberId}");
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<CartItemsDTO>>(responseContent);

                    if (data == null)
                    {
                        return View("Error");
                    }
                    order.OrderItem = data.Select(ci => new OrderItem
                    {
                        ProductName = ci.ProductName,
                        Amount = ci.Qty,
                        SubTotal = ci.Qty * ci.Price
                    }).ToList();

                    var orders = new Orders()
                    {
                        MemberId = order.MemberId,
                        OrderNumber = Guid.NewGuid().ToString("N"),
                        CreateAt = DateTime.Now,
                        TradeStatus = 0,
                        OrderType = 1,
                        OrderStatus = 0,
                        Amount = order.OrderItem.Sum(ci => ci.SubTotal),
                        OrderContent = JsonConvert.SerializeObject(order.OrderItem),
                        ShippingMethod = order.ShippingMethod,
                        OrderAddress = order.OrderAddress,

                    };

                    var content = new StringContent(
                        JsonConvert.SerializeObject(orders),
                        Encoding.UTF8,
                        "application/json"
                    );

                    var url = "https://localhost:7040/api/Orders";

                    var res = await httpClient.PostAsync(url, content);

                    if (res.IsSuccessStatusCode)
                    {
                        var resContent = await res.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Orders>(resContent);
                        //todo 清除購物車

                        return RedirectToAction("ReviewOrder", new { Id = result.Id });
                    }
                    else
                    {
                        throw new Exception($"Failed to create order: {res.ReasonPhrase}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Checkout");
            }
        }

        public async Task<IActionResult> ReviewOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"https://localhost:7040/api/Orders/{id}");
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Order>(responseContent);

                    if (data == null)
                    {
                        return View("Error");
                    }

                    data.OrderItem = JsonConvert.DeserializeObject<List<OrderItem>>(data.OrderContent);
                    ViewBag.orderItems = data.OrderItem;

                    return View(data);
                }
            }
            catch (HttpRequestException ex)
            {
                return View("Error");
            }
        }

        // 付款
        public async Task<IActionResult> Payment(int? id, bool isSuccess)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {

                var response = await httpClient.PutAsync($"https://localhost:7040/api/Orders/IsPaid/{id}", null); // 使用 PUT 方法呼叫 API

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return RedirectToAction("ReviewOrder", new { Id = id });
                }
            }

            //var order = await _context.Order.FirstOrDefaultAsync(p => p.Id == Id);
            //if (order == null)
            //{
            //    return NotFound();
            //}
            //else
            //{
            //    if (isSuccess)
            //    {
            //        order.isPaid = true;
            //        _context.Update(order);
            //        await _context.SaveChangesAsync();  //更新訂單狀態
            //    }
            //    return RedirectToAction("ReviewOrder", new { Id = order.Id });
            //}
        }
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

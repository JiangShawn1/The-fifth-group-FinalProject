using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
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
                        SubTotal = ci.Price,
                    }).ToList();

                    var orders = new Orders()
                    {
                        MemberId = order.MemberId,
                        OrderNumber = Guid.NewGuid().ToString("N"),
                        CreateAt = DateTime.Now,
                        TradeStatus = 0,
                        OrderType = 1,
                        OrderStatus = 0,
                        Amount = order.OrderItem.Sum(ci => ci.SubTotal * ci.Amount),
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

                        var delrep = await httpClient.DeleteAsync($"https://localhost:7040/api/Orders/ClearCart/{order.MemberId}");
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ReviewOrder", new { Id = result.Id });
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode);
                        }
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
            if (isSuccess)
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.PutAsync($"https://localhost:7040/api/Orders/IsPaid/{id}", null);
                }
            }
            return RedirectToAction("Index");
        }
    }
}

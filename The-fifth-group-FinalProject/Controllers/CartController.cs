using Microsoft.AspNetCore.Mvc;
using The_fifth_group_FinalProject.Helpers;
using The_fifth_group_FinalProject.Models;

namespace The_fifth_group_FinalProject.Controllers
{
    public class CartController : Controller
    {
        public IActionResult RemoveItem(int id)
        {
            //向 Session 取得商品列表
            List<PCartItem> cart = SessionHelper.
                GetObjectFromJson<List<PCartItem>>(HttpContext.Session, "cart");

            //用FindIndex查詢目標在List裡的位置
            int index = cart.FindIndex(m => m.Product.Id.Equals(id));
            cart.RemoveAt(index);

            if (cart.Count < 1)
            {
                SessionHelper.Remove(HttpContext.Session, "cart");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            //向 Session 取得商品列表
            List<PCartItem> CartItems = SessionHelper.
                GetObjectFromJson<List<PCartItem>>(HttpContext.Session, "cart");

            //計算商品總額
            if (CartItems != null)
            {
                ViewBag.Total = CartItems.Sum(m => m.SubTotal);
            }
            else
            {
                ViewBag.Total = 0;
            }

            return View(CartItems);
        }
    }
}

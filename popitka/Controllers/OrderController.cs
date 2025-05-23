using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.Domain.Models;
using tweb.DAL.Data;
using popitka.ViewModels;
using System.Collections.Generic;

namespace popitka.Controllers
{
    public class OrderController : Controller
    {
        [HttpGet]
        public ActionResult Checkout() => View();

        [HttpPost]
        public ActionResult CheckoutConfirmed(FormCollection form)
        {
            if (Session["UserId"] == null) return RedirectToAction("Login", "Account");

            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any()) return RedirectToAction("Index", "Cart");

            var order = new Order
            {
                UserId = (int)Session["UserId"],
                Items = cart.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList(),
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ShippingAddress = $"{form["Region"]}, {form["City"]}, {form["Street"]}, {form["Zip"]}"
            };

            using (var db = new AppDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }

            Session["Cart"] = null;
            return RedirectToAction("Order", "Order");
        }

        public ActionResult Order()
        {
            int userId = (int)Session["UserId"];
            using (var db = new AppDbContext())
            {
                var orders = db.Orders.Where(o => o.UserId == userId).ToList();
                return View("Order", orders);
            }
        }

        public ActionResult Shipping() => View();
    }

}

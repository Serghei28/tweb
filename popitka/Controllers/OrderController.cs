using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.Domain.Models;
using popitka.ViewModels;
using YourProject.BusinessLogic.Interfaces;
using System.Collections.Generic;

namespace popitka.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult Checkout() => View();

        [HttpPost]
        public ActionResult CheckoutConfirmed(FormCollection form)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Account");

            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            var userId = (int)Session["UserId"];

            var orderItems = cart.Select(c => new OrderItem
            {
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                Price = c.Price
            }).ToList();

            var shippingAddress = $"{form["Region"]}, {form["City"]}, {form["Street"]}, {form["Zip"]}";

            // Можно сделать метод CreateOrder с адресом строкой, если не используется ID.
            var order = _orderService.CreateOrder(userId, orderItems, 0); // передаётся 0 если AddressId не используется
            order.ShippingAddress = shippingAddress;

            Session["Cart"] = null;
            return RedirectToAction("Order", "Order");
        }

        public ActionResult Order()
        {
            int userId = (int)Session["UserId"];
            var orders = _orderService.GetUserOrders(userId);
            return View("Order", orders);
        }

        public ActionResult Shipping() => View();
    }
}

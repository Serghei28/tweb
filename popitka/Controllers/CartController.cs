using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YourProject.BusinessLogic.Implementation;
using YourProject.Domain.Models;

namespace popitka.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService = new ProductService();

        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            Session["Cart"] = cart;
            return cart;
        }

        public ActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductById(productId);
            if (product == null) return HttpNotFound();

            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            Session["Cart"] = cart;

            return RedirectToAction("Cart");
        }

        public ActionResult Cart()
        {
            var cart = GetCart();
            return View("Cart", cart);
        }
    }
}

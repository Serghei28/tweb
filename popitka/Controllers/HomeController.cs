using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.Domain.Models;
using tweb.DAL.Data;
using popitka.ViewModels;
using System.Collections.Generic;

namespace popitka.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AppDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(string fullName, string email, string password)
        {
            var parts = fullName.Trim().Split(' ');
            string firstName = parts.Length > 0 ? parts[0] : "";
            string lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

            var user = new User
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Phone = "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsAdmin = false
            };

            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    Session["UserId"] = user.Id;
                    Session["UserName"] = user.FirstName;
                    Session["IsAdmin"] = user.IsAdmin;
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Error = "Неверный email или пароль";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            int userId = Convert.ToInt32(Session["UserId"]);
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null)
                    return RedirectToAction("Login");

                return View(user);
            }
        }

        public ActionResult AddOrEditProduct(int? id)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login");

            using (var db = new AppDbContext())
            {
                var product = id.HasValue ? db.Products.Find(id.Value) : new Product();
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(Product product, string imageInput)
        {
            if (!ModelState.IsValid)
                return View(product);

            if (!string.IsNullOrWhiteSpace(imageInput))
                product.ImageUrl = imageInput.Split(',').Select(x => x.Trim()).FirstOrDefault() ?? "/Content/Images/default.png";

            using (var db = new AppDbContext())
            {
                if (product.Id == 0)
                {
                    product.CreatedAt = DateTime.UtcNow;
                    db.Products.Add(product);
                }
                else
                {
                    var existing = db.Products.Find(product.Id);
                    if (existing != null)
                    {
                        existing.Name = product.Name;
                        existing.Description = product.Description;
                        existing.Price = product.Price;
                        existing.Stock = product.Stock;
                        existing.ImageUrl = product.ImageUrl;
                        existing.Category = product.Category;
                        existing.UpdatedAt = DateTime.UtcNow;
                    }
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return new HttpUnauthorizedResult();

            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }

            return Json(new { success = true });
        }

        public ActionResult AdminPanel()
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login");

            using (var db = new AppDbContext())
            {
                var model = new AdminPanelViewModel
                {
                    Orders = db.Orders.ToList(),
                    Users = db.Users.ToList()
                };
                return View(model);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product == null)
                    return HttpNotFound();

                return View(product);
            }
        }

        private List<CartItem> GetCart()
        {
            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }



        public ActionResult AddToCart(int productId)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(productId);
                if (product == null)
                    return HttpNotFound();

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
            }

            return RedirectToAction("Cart");
        }


        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckoutConfirmed(FormCollection form)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
                return RedirectToAction("Cart");

            string firstName = form["FirstName"] ?? "";
            string lastName = form["LastName"] ?? "";
            string region = form["Region"] ?? "";
            string city = form["City"] ?? "";
            string street = form["Street"] ?? "";
            string zip = form["Zip"] ?? "";
            string payment = form["PaymentMethod"] ?? "";

            string fullAddress = $"{region}, {city}, {street}, {zip}";

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
                ShippingAddress = fullAddress
            };

            using (var db = new AppDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }

            Session["Cart"] = null;
            return RedirectToAction("Order");
        }


        public ActionResult Cart()
        {
            var cart = Session["Cart"] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult Order()
        {
            int userId = (int)Session["UserId"];
            using (var db = new AppDbContext())
            {
                var orders = db.Orders
                               .Where(o => o.UserId == userId)
                               .ToList();

                return View(orders);
            }
        }


        public ActionResult Shipping() => View();
        public ActionResult Support() => View();
       
        public ActionResult About() => View();
        
        public ActionResult LayoutFooter() => View();
    }
}

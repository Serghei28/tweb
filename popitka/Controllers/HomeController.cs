using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.Domain.Models;
using tweb.DAL.Data;
using popitka.ViewModels;

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

        // Регистрация
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

        // Логин
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

        // Профиль пользователя
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

        // Создание или редактирование товара (только для админа)
        public ActionResult AddOrEditProduct(int? id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
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

            // Обновляем ImageUrl
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

        // Удаление продукта (AJAX)
        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }



        // Админ-панель
        public ActionResult AdminPanel()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
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

        // Остальные страницы
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

        public ActionResult Shipping() => View();
        public ActionResult Support() => View();
        public ActionResult Cart() => View();
        public ActionResult About() => View();
      
        public ActionResult Checkout() => View();
        public ActionResult Order() => View();
        public ActionResult LayoutFooter() => View();
    }
}

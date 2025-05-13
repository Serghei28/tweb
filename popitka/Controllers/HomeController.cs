using System;
using System.Web.Mvc;
using YourProject.Domain.Models;
using tweb.DAL.Data;
using System.Linq;


namespace popitka.Controllers
{
    public class HomeController : Controller
    {
        // Главная страница
        public ActionResult Index()
        {
            return View();
        }

        // Страница регистрации
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string fullName, string email, string password)
        {
            var parts = fullName.Trim().Split(' ');
            string firstName = parts.Length > 0 ? parts[0] : "";
            string lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : "";

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Phone = "", // если поля нет, можно оставить пустым
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Shipping()
        {
            return View();
        }
        public ActionResult Support()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Ipad()
        {
            return View();
        }
        public ActionResult Macbook()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult LayoutFooter()
        {
            return View();
        }
    }

}

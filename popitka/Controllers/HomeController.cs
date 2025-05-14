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
                Password = password, // ← ВАЖНО: сохраняем пароль
                FirstName = firstName,
                LastName = lastName,
                Phone = "", // или добавь поле, если нужно
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

                return View(user); // ← передаём в представление
            }
        }





        public ActionResult LayoutFooter()
        {
            return View();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tweb.DAL.Data;
using YourProject.Domain.Models;

namespace popitka.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(string fullName, string email, string password)
        {
            var names = fullName.Trim().Split(' ');
            var user = new User
            {
                Email = email,
                Password = password,
                FirstName = names.FirstOrDefault() ?? "",
                LastName = string.Join(" ", names.Skip(1)),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
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
                    return RedirectToAction("Index", "Product");
                }
            }

            ViewBag.Error = "Неверный email или пароль";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Product");
        }

        public ActionResult UserProfile()
        {
            if (Session["UserId"] == null) return RedirectToAction("Login");

            int userId = (int)Session["UserId"];
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                return View(user);
            }
        }
    }

}
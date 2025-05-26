using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using YourProject.BusinessLogic.Interfaces;
using YourProject.Domain.Models;

namespace popitka.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Register() => View();

        [HttpPost]
        public async Task<ActionResult> Register(string fullName, string email, string password)
        {
            var names = fullName.Trim().Split(' ');
            var user = new User
            {
                Email = email,
                Password = password,
                FirstName = names.Length > 0 ? names[0] : "",
                LastName = names.Length > 1 ? string.Join(" ", names, 1, names.Length - 1) : "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userService.UpdateUser(0, user); // Предположим, UpdateUser создаёт, если id == 0

            return RedirectToAction("Login");
        }

        public ActionResult Login() => View();

        [HttpPost]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await _userService.GetUserByCredentials(email, password); // ✅
            if (user != null && user.Email == email && user.Password == password)
            {
                Session["UserId"] = user.Id;
                Session["UserName"] = user.FirstName;
                Session["IsAdmin"] = user.IsAdmin;
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Error = "Неверный email или пароль";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Product");
        }

        public async Task<ActionResult> UserProfile()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            int userId = (int)Session["UserId"];
            var userWithAddresses = await _userService.GetUserWithAddresses(userId);
            return View(userWithAddresses);
        }
    }
}

using System.Threading.Tasks;
using System.Web.Mvc;
using popitka.ViewModels;
using YourProject.BusinessLogic.Interfaces;

namespace popitka.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AdminController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result)
                return RedirectToAction("AdminPanel");
            else
                return HttpNotFound();
        }
        public async Task<ActionResult> DeleteUser(int id)
        {
            bool result = await _userService.DeleteUser(id);

            if (result)
                return RedirectToAction("AdminPanel");
            else
                return HttpNotFound();
        }
        [HttpPost]
        public async Task<ActionResult> SetAdmin(int id)
        {
            var result = await _userService.SetAdminRole(id, true);
            if (result)
                return RedirectToAction("AdminPanel");
            else
                return HttpNotFound();
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAdmin(int id)
        {
            var result = await _userService.SetAdminRole(id, false);
            if (result)
                return RedirectToAction("AdminPanel");
            else
                return HttpNotFound();
        }


        public ActionResult AdminPanel()
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login", "Account");

            var model = new AdminPanelViewModel
            {
                Users = _userService.GetAllUsers().Result,
                Orders = _orderService.GetAllOrders()
            };

            return View(model);
        }
    }
}

using popitka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tweb.DAL.Data;

namespace popitka.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminPanel()
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login", "Account");

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
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tweb.DAL.Data;
using YourProject.Domain.Models;

namespace popitka.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new AppDbContext())
            {
                var products = db.Products.ToList();
                return View(products);
            }
        }

        public ActionResult ProductDetails(int id)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product == null) return HttpNotFound();
                return View(product);
            }
        }

        public ActionResult AddOrEditProduct(int? id)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login", "Account");

            using (var db = new AppDbContext())
            {
                var product = id.HasValue ? db.Products.Find(id.Value) : new Product();
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(Product product, string imageInput)
        {
            if (!ModelState.IsValid) return View(product);

            if (!string.IsNullOrWhiteSpace(imageInput))
                product.ImageUrl = imageInput.Split(',').FirstOrDefault()?.Trim() ?? "/Content/Images/default.png";

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
        public ActionResult Delete(int id)
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
    }

}
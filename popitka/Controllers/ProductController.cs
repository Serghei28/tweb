using System;
using System.Linq;
using System.Web.Mvc;
using YourProject.BusinessLogic.Implementation;
using YourProject.BusinessLogic.Interfaces;
using YourProject.Domain.Models;

namespace popitka.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        public ActionResult Index()
        {
            var products = _productService.GetAllProducts();
            return View(products);
        }

        public ActionResult ProductDetails(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        public ActionResult AddOrEditProduct(int? id)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login", "Account");

            Product product;

            if (id.HasValue)
            {
                product = _productService.GetProductById(id.Value);
                if (product == null)
                    return HttpNotFound();
            }
            else
            {
                product = new Product();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult AddOrEditProduct(Product product)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            if (string.IsNullOrWhiteSpace(product.ImageUrl))
                product.ImageUrl = "/Content/Images/default.png";

            if (product.Id == 0)
            {
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                var existingProduct = _productService.GetProductById(product.Id);
                if (existingProduct == null)
                    return HttpNotFound();

                product.CreatedAt = existingProduct.CreatedAt;
                product.UpdatedAt = DateTime.UtcNow;
            }

            _productService.SaveProduct(product);

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!(Session["IsAdmin"] is bool isAdmin) || !isAdmin)
                return new HttpUnauthorizedResult();

            _productService.DeleteProduct(id);
            return Json(new { success = true });
        }
    }
}

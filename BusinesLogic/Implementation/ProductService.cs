using System;
using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;
using tweb.DAL.Data;

namespace YourProject.BusinessLogic.Implementation
{
    public class ProductService : IProductService
    {
        public Product GetProductById(int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Products.Find(id);
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var db = new AppDbContext())
            {
                return db.Products.ToList();
            }
        }

        public void SaveProduct(Product product)
        {
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
        }

        public void DeleteProduct(int id)
        {
            using (var db = new AppDbContext())
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
        }
    }
}
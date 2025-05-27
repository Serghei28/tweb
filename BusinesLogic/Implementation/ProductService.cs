using System;
using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;
using tweb.DAL.Data;
using System.Data.Entity;

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
                    db.Entry(product).State = EntityState.Modified;
                    product.UpdatedAt = DateTime.UtcNow;
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
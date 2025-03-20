using System.Collections.Generic;
using System.Linq;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.API
{
    public class ProductAPI : BaseAPI
    {
        private readonly List<Product> _products;

        public ProductAPI()
        {
            _products = new List<Product>
            {
                new Product(1, "Laptop", "High-end laptop", 1200, ProductCategory.Mac, null, null, 10),
                new Product(2, "Smartphone", "Latest smartphone", 800, ProductCategory.iPhone, null, null, 15),
                new Product(3, "Coffee Maker", "Brews coffee", 150, ProductCategory.iPad, null, null, 5)
            };
        }

        public Product GetProduct(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductsByCategory(ProductCategory category)
        {
            return _products.Where(p => p.Category == category).ToList();
        }
    }
}

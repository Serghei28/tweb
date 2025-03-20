using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourProject.Domain.Models;
using YourProject.BusinessLogic.Interfaces;

namespace YourProject.BusinessLogic.Implementation
{
    public class ProductService : IProductService
    {
        // Временное хранилище продуктов для демонстрации
        private readonly List<Product> _products = new List<Product>();

        public ProductService()
        {
            // Инициализация демонстрационными данными
            _products.Add(new Product(1, "MacBook Pro", "Apple laptop", 1999.99m, ProductCategory.Mac, null, null, 10));
            _products.Add(new Product(2, "iPhone 13", "Apple smartphone", 999.99m, ProductCategory.iPhone, null, null, 20));
            _products.Add(new Product(3, "iPad Pro", "Apple tablet", 799.99m, ProductCategory.iPad, null, null, 15));
        }

        public async Task<IEnumerable<Product>> GetProducts(ProductCategory category)
        {
            // Симуляция асинхронного выполнения
            return await Task.Run(() => _products.Where(p => p.Category == category));
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await Task.Run(() => _products.FirstOrDefault(p => p.Id == id));
            if (product == null)
                throw new System.Exception("Product not found");
            return product;
        }

        public async Task<IEnumerable<Product>> SearchProducts(string query)
        {
            return await Task.Run(() => _products.Where(p => p.Name.Contains(query) || p.Description.Contains(query)));
        }

        public async Task<IEnumerable<Product>> GetRelatedProducts(int productId)
        {
            // Для демонстрации предполагаем, что связанные продукты имеют ту же категорию
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                throw new System.Exception("Product not found");
            return await Task.Run(() => _products.Where(p => p.Category == product.Category && p.Id != productId));
        }

        public async Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<int> ids)
        {
            return await Task.Run(() => _products.Where(p => ids.Contains(p.Id)));
        }
    }
}

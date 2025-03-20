using System.Collections.Generic;
using System.Threading.Tasks;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(ProductCategory category);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> SearchProducts(string query);
        Task<IEnumerable<Product>> GetRelatedProducts(int productId);
        Task<IEnumerable<Product>> GetProductsByIds(IEnumerable<int> ids);
    }
}

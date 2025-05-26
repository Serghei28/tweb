using System.Collections.Generic;
using YourProject.Domain.Models;

namespace YourProject.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Product GetProductById(int id);
        List<Product> GetAllProducts();
        void SaveProduct(Product product);
        void DeleteProduct(int id);
    }
}
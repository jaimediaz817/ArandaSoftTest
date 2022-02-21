using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(ProductQueryFilter filters);
        Task<IEnumerable<Category>> GetCategories();
        Task InsertProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByDescription(string description);
        Task<IEnumerable<Product>> GetProductByCategoryId(int id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
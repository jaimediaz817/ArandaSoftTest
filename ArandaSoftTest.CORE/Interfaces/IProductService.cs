using ArandaSoftTest.CORE.CustomEntities;
using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IProductService
    {
        //IEnumerable<Product> GetProducts(ProductQueryFilter filters);
        PagedList<Product> GetProducts(ProductQueryFilter filters);
        IEnumerable<Category> GetCategories();
        Task InsertProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsByCategoryId(int id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}
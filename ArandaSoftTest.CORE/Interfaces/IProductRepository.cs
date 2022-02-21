
using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(ProductQueryFilter filters);
        Task<IEnumerable<Category>> GetCategories();
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByDescription(string description);
        Task<IEnumerable<Product>> GetProductByCategoryId(int id);
        //public Task<Product> GetProductByDescription(int description);
        //public Task<Product> GetProductByCategory(int categoryId);


        Task<Category> GetCategoryById(int id);

        Task InsertProduct(Product product);

        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
    }
}

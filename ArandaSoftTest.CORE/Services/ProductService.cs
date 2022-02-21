using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // operaciones
        public async Task<IEnumerable<Product>> GetProducts(ProductQueryFilter filters)
        {
            var products = _productRepository.GetProducts(filters);

            return await products;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _productRepository.GetProductByName(name);
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryId(int id)
        {
            return await _productRepository.GetProductByCategoryId(id);
        }

        public async Task<IEnumerable<Product>> GetProductByDescription(string description)
        {
            return await _productRepository.GetProductByDescription(description);
        }

        public async Task InsertProduct(Product product)
        {
            var category = await _productRepository.GetCategoryById((int)product.CategoryId);

            // Regla de negocio ARANDASOFT: evidenciar manejo de errores
            if (category == null)
            {
                throw new Exception("La categoría no existe.");
            }

            await _productRepository.InsertProduct(product);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _productRepository.GetCategories();
        }
    }
}

using ArandaSoftTest.CORE.CustomEntities;
using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.CORE.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Services
{
    public class ProductService : IProductService
    {
        //private readonly IProductRepository _productRepository;
        //private readonly IRepository<Product> _productRepository;
        //private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;
        public ProductService(IUnitOfWork unitOfWork, IOptions<PaginationOptions>options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

        // operaciones
        public PagedList<Product> GetProducts(ProductQueryFilter filters)
        {
            // gestionando entrada de filtros por defecto
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var products = _unitOfWork.ProductRepository.GetAll();

            if (filters.CategoryId != null)
            {
                products = products.Where(p => p.CategoryId == filters.CategoryId);
            }

            if (filters.Name != null)
            {
                products = products.Where(p => p.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if (filters.Description != null)
            {
                products = products.Where(p => p.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            // Paginación
            var pagedPosts = PagedList<Product>.Create(products, filters.PageNumber, filters.PageSize);

            return pagedPosts;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int id)
        {
            var productsByCat = await _unitOfWork.ProductRepository.GetProductsByCategory(id);
            return productsByCat;
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _unitOfWork.ProductRepository.GetById(id);
        }        

        public async Task InsertProduct(Product product)
        {
            var category = await _unitOfWork.CategoryRepository.GetById((int)product.CategoryId);

            // Regla de negocio ARANDASOFT: evidenciar manejo de errores
            if (category == null)
            {
                throw new Exception("La categoría no existe.");
            }

            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteProduct(int id)
        {
            await _unitOfWork.ProductRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}

﻿using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.CORE.QueryFilters;
using ArandaSoftTest.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.INFRASTRUCTURE.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private PruebaArandaSoftContext _context;        

        public ProductRepository(PruebaArandaSoftContext context)
        {
            _context = context;
        }

        // TODO: no funcionan los filtros requeridos en la prueba, comentar de momento y evaluar contra el patrón UnitOfWork para unificar Repositorios en una unidad de trabajo
        public async Task<IEnumerable<Product>> GetProducts(ProductQueryFilter filters)
        {

            var products = await _context.Product.Include(p => p.Category).ToListAsync();

            if (filters.CategoryId != null)
            {
                //products = (List<Product>)products.Where(p => p.CategoryId == filters.CategoryId);
            }

            if (filters.Name != null)
            {
                //products = (List<Product>)products.Where(p => p.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if (filters.Description != null)
            {
                //products = (List<Product>)products.Where(p => p.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var post = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        // TODO: refactorizar poneindo esto en otro repositorio para Categoría
        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        /*public async Task<Product> GetProductByName(string name)
        {
            var post = await _context.Product.FirstOrDefaultAsync(p => p.Name == name);
            return post;
        }*/


        public async Task InsertProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var currentProduct = await GetProductById(product.Id);
            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.Image = product.Image;
            currentProduct.CategoryId = product.CategoryId;

            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var currentProduct = await GetProductById(id);
            _context.Product.Remove(currentProduct);
            int rows = await _context.SaveChangesAsync();

            return rows > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _context.Product.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryId(int id)
        {
            return await _context.Product.Where(x => x.CategoryId == id).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByDescription(string description)
        {
            return await _context.Product.Where(x => x.Description == description).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _context.Category.OrderByDescending(x => x.Name).ToListAsync();
            return categories;
        }
    }
}

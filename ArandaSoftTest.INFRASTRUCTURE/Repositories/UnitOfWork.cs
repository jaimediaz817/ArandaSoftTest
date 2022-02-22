using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.INFRASTRUCTURE.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.INFRASTRUCTURE.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private PruebaArandaSoftContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Category> _categoryRepository;

        public UnitOfWork(PruebaArandaSoftContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository => _productRepository ?? new ProductRepository(_context);

        public IRepository<Category> CategoryRepository => _categoryRepository ?? new BaseRepository<Category>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveShanges()
        {
            _context.SaveChanges();
        }
    }
}

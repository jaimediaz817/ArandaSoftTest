using ArandaSoftTest.CORE.Entities;
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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        // enviando al contructor del padre el contexto
        public ProductRepository(PruebaArandaSoftContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Product>> GetProductsByCategory(int id)
        {
            return await _entities.Where(x => x.CategoryId == id).ToListAsync();
        }
    }
}

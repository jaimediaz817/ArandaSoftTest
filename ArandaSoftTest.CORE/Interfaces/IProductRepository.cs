
using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(int id);
    }
}

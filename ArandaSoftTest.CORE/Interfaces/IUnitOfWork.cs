using ArandaSoftTest.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }

        void SaveShanges();

        Task SaveChangesAsync();

    }
}

using ArandaSoftTest.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IRepository<Category> CategoryRepository { get; }

        void SaveShanges();

        Task SaveChangesAsync();

    }
}

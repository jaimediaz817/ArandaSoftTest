using ArandaSoftTest.CORE.Entities;
using ArandaSoftTest.CORE.Interfaces;
using ArandaSoftTest.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.INFRASTRUCTURE.Repositories
{

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private PruebaArandaSoftContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(PruebaArandaSoftContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        // permitir realizar filtros antes de ...
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
            //await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            //await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
            //_context.SaveChanges();
        }
    }
}

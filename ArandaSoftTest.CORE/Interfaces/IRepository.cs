using ArandaSoftTest.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArandaSoftTest.CORE.Interfaces
{
    // restricción: solo crear repos de las entidades de negocio de nostoros, que hayamos matriculado y estemos mapeando contra una tabla <> base entity que todas heredan de este
    public interface IRepository<T> where T : BaseEntity 
    {
        // mapeando el crud
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);        
        void Update(T entity);
        Task Delete(int id);
    }
}

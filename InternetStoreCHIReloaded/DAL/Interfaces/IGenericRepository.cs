using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll(Func<T, bool> predicate);
        T FindFirstOrDefault(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(int entityId);
        int Save();
    }
}

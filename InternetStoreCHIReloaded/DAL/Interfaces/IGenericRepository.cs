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
        bool Add(T entity);
        bool Update(T entity);
        bool Remove(int entityId);
    }
}

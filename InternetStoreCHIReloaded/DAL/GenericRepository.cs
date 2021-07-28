using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private readonly StoreContext _dbcontext;
        private DbSet<T> _fieldOfWork;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;

            _fieldOfWork = dbcontext.Set<T>();
        }

        public bool Add(T entity)
        {
            _fieldOfWork.Add(entity);

            return Save();
        }

        public IEnumerable<T> FindAll(Func<T, bool> predicate)
        {
            return _fieldOfWork.Where(predicate).ToList();
        }

        public T FindFirstOrDefault(Func<T, bool> predicate)
        {
            return _fieldOfWork.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _fieldOfWork.ToList();
        }

        public bool Remove(int entityId)
        {
            T entity = new T();
            entity.Id = entityId;

            _fieldOfWork.Attach(entity);
            _fieldOfWork.Remove(entity);

            return Save();
        }

        public bool Update(T entity)
        {
            _fieldOfWork.Update(entity);

            return Save();
        }

        private bool Save()
        {
            return _dbcontext.SaveChanges() > 0;
        }
    }
}

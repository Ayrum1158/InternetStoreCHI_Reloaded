using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbcontext;
        private DbSet<T> fieldOfWork;

        public GenericRepository(StoreContext dbcontext)
        {
            this.dbcontext = dbcontext;

            fieldOfWork = dbcontext.Set<T>();
        }

        public void Add(T entity)
        {
            fieldOfWork.Add(entity);
        }

        public IEnumerable<T> FindAll(Func<T, bool> predicate)
        {
            return fieldOfWork.Where(predicate).ToList();
        }

        public T FindFirst(Func<T, bool> predicate)
        {
            return fieldOfWork.Where(predicate).First();// exception if none is better then null
        }

        public IEnumerable<T> GetAll()
        {
            return fieldOfWork.ToList();
        }

        public void Remove(int entityId)
        {
            T entity = (T)new BaseEntity();
            entity.Id = entityId;

            fieldOfWork.Attach(entity);
            fieldOfWork.Remove(entity);
        }

        public int Save()
        {
            return dbcontext.SaveChanges();
        }

        public void Update(T entity)
        {
            fieldOfWork.Update(entity);
        }
    }
}

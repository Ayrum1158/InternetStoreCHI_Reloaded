using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<bool> Add(T entity)
        {
            _fieldOfWork.Add(entity);

            return await Save();
        }

        public async Task<IEnumerable<T>> FindAll(Func<T, bool> predicate)
        {
            return _fieldOfWork.Where(predicate).ToList();
        }

        public async Task<T> FindFirstOrDefault(Func<T, bool> predicate)
        {
            return _fieldOfWork.Where(predicate).FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _fieldOfWork.ToList();
        }

        public async Task<bool> Remove(int entityId)
        {
            T entity = new T();
            entity.Id = entityId;

            _fieldOfWork.Attach(entity);
            _fieldOfWork.Remove(entity);

            bool success = true;
            try
            {
                success = await Save();
            }
            catch (DbUpdateConcurrencyException)// when trying to delete with id not in Db
            {
                success = false;
            }

            return success;
        }

        public async Task<bool> Update(T entity)
        {
            _fieldOfWork.Update(entity);

            return await Save();
        }

        private async Task<bool> Save()
        {
            return _dbcontext.SaveChanges() > 0;
        }
    }
}

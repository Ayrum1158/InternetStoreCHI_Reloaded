using DAL.Entities;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IHasId, new()
    {
        protected readonly StoreContext _dbcontext;
        protected DbSet<T> _fieldOfWork;

        public GenericRepository(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;

            _fieldOfWork = dbcontext.Set<T>();
        }

        public virtual async Task<DbResponse<T>> AddAsync(T entity)
        {
            await _fieldOfWork.AddAsync(entity);

            bool success = await SaveAsync();

            return new DbResponse<T>()
            {
                IsSuccessful = success,
                Data = entity
            };
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate)
        {
            return await Task.Run(() => _fieldOfWork.Where(predicate).ToList());
        }

        public virtual async Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _fieldOfWork.FirstOrDefaultAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _fieldOfWork.ToListAsync();
        }

        public virtual async Task<bool> IsPresentInDbAsync(Expression<Func<T, bool>> expression)
        {
            var res = await _fieldOfWork.AnyAsync(expression);
            //var res = (await FindFirstOrDefaultAsync(expression)) != null;
            return res;
        }

        public virtual async Task<bool> RemoveAsync(int entityId)
        {
            T entity = new T();
            entity.Id = entityId;

            _fieldOfWork.Attach(entity);
            _fieldOfWork.Remove(entity);

            bool success = true;
            try
            {
                success = await SaveAsync();
            }
            catch (DbUpdateConcurrencyException)// when trying to delete with id not in Db
            {
                success = false;
            }

            return success;
        }

        protected async Task<bool> SaveAsync()
        {
            return await _dbcontext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _fieldOfWork.Update(entity);

            return await SaveAsync();
        }
    }
}

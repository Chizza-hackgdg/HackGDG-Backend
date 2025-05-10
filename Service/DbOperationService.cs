using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entity;
using Service;
using Core.Entities;

namespace Service
{
    public class DbOperationService<T, TId> where T : IEntity<TId>, new()
    {
        private readonly ITBaseService<T,TId> _service;
        public DbOperationService(ITBaseService<T, TId> service)
        {
            _service = service;
        }
        public async Task<T> AddAsync(T entity)
        {
            return await _service.AddAsync(entity);
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            return await _service.UpdateAsync(entity);
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            return await _service.DeleteAsync(entity);
        }
        public T Add(T entity)
        {
            return _service.Add(entity);
        }
        public bool Update(T entity)
        {
            return _service.Update(entity);
        }
        public bool Delete(T entity)
        {
            return _service.Delete(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _service.GetAll();
        }
        public IList<T> GetAllList(Expression<Func<T, bool>> predicate)
        {
            return _service.GetAllList(predicate);
        }
        public async Task<IList<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await _service.GetAllListAsync(predicate);
        }
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _service.FirstOrDefault(predicate);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _service.FirstOrDefaultAsync(predicate);
        }
    }
}
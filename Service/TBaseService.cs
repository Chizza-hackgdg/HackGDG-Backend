using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entity;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Service
{
    public interface ITBaseService<T, TId> where T : IEntity<TId>, new()
    {
        Task<T> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        // queryable
        IQueryable<T> GetAll();
        IList<T> GetAllList(Expression<Func<T, bool>>? predicate = null);
        Task<IList<T>> GetAllListAsync(Expression<Func<T, bool>>? predicate = null);
        T FirstOrDefault(Expression<Func<T, bool>>? predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null);
    }

    public class TBaseService<T, TId> : ITBaseService<T, TId> where T : IEntity<TId>, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public TBaseService(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _dbSet = context.Set<T>();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<T> AddAsync(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = Guid.Parse(_userManager.GetUserId(user)!);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }
            var userId = _userManager.GetUserId(user);
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = Guid.Parse(userId!);
            var existingEntity = await _dbSet.FindAsync(entity.Id);

            if (existingEntity != null)
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    var newValue = property.GetValue(entity);
                    if (newValue != null)
                    {
                        property.SetValue(existingEntity, newValue);
                    }
                }
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }
            entity.DeletedDate = DateTime.Now;
            var existingEntity = await _dbSet.FindAsync(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable().Where(y => y.IsDeleted == false);
        }

        public IList<T> GetAllList(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null ? GetAll().ToList() : GetAll().Where(predicate).ToList();
        }

        public async Task<IList<T>> GetAllListAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null ? await GetAll().ToListAsync() : await GetAll().Where(predicate).ToListAsync();
        }

        public T FirstOrDefault(Expression<Func<T, bool>>? predicate = null)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public T Add(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }

            entity.CreatedDate = DateTime.Now;

            _dbSet.Add(entity);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return entity;
        }

        public bool Update(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }

            entity.UpdatedDate = DateTime.Now;
            var existingEntity = _dbSet.Find(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Delete(T entity)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("Bu işlemi yapabilmek için giriş yapmanız gerekiyor.");
            }
            entity.DeletedDate = DateTime.Now;
            var existingEntity = _dbSet.Find(entity.Id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
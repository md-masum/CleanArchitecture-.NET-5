using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual bool Add(TEntity entity)
        {
            _context.Add(entity);
            return SaveChange();
        }
        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            _context.Add(entity);
            return await SaveChangeAsync();
        }

        public virtual bool Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return SaveChange();
        }
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await SaveChangeAsync();
        }

        public virtual bool Remove(TEntity entity)
        {
            _context.Remove(entity);
            return SaveChange();
        }
        public virtual async Task<bool> RemoveAsync(TEntity entity)
        {
            _context.Remove(entity);
            return await SaveChangeAsync();
        }

        public virtual TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }
        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual ICollection<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).ToList();
        }
        public virtual async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual IQueryable<TEntity> GetAsQueryable()
        {
            return _context
                .Set<TEntity>().AsQueryable();
        }

        public virtual bool UpdateRange(ICollection<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
            return SaveChange();
        }
        public virtual async Task<bool> UpdateRangeAsync(ICollection<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
            return await SaveChangeAsync();
        }

        public bool RemoveRange(ICollection<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
            return SaveChange();
        }
        public async Task<bool> RemoveRangeAsync(ICollection<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
            return await SaveChangeAsync();
        }

        private bool SaveChange()
        {
            return _context.SaveChanges() > 0;
        }

        private async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

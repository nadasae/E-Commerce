using E_Commece.DA.Context;
using E_Commerce.Core.Entitiies;
using E_Commerce.Core.Interfaces.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commece.DA.Implementations.Base
{
    public class BaseRepository<TEntity, Key> : IBaseRepository<TEntity, Key>
     where Key : struct
     where TEntity : Entity<Key>
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _table;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _table = _dbContext.Set<TEntity>();
        }
        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = GetWhere(criteria, includes);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(int skip, int take, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);

            var items = await query.Skip(skip).Take(take).ToListAsync();

            return query.ToList();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria, int? skip, int? take, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }
        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? criteria = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _table;

            if (criteria != null)
                query = query.Where(criteria);

            query = Includes(query, includes);

            return await query.ToListAsync();
        }
        public Task<TEntity?> GetByIdAsync(Key id)
        {
            return _table.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }
        public Task<TEntity?> GetByIdAsync(Key id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);
            return query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.AnyAsync() : await _table.AnyAsync(criteria);
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? criteria = null)
        {
            return criteria == null ? await _table.CountAsync() : await _table.CountAsync(criteria);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        private IQueryable<TEntity> Includes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query;
        }
        private IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> criteria, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = Includes(_table, includes);

            return query.Where(criteria);
        }
    }
}

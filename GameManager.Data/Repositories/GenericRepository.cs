using GameManager.Domain.Contracts.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GameManager.Data.Repositories
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual TEntity Get(string id)
        {
            return _dbSet.Find(id);
        }

        public virtual TEntity Get(string id, string[] includes)
        {
            foreach (var item in includes)
                _dbSet.Include(item);

            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, Boolean> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, Boolean> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var item in includes)
                _dbSet.Include(item).Load();

            return _dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual TEntity FirstOrDefault(Func<TEntity, Boolean> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public TEntity Get(string id, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var item in includes)
                _dbSet.Include(item).Load();

            return _dbSet.Find(id);
        }
    }
}

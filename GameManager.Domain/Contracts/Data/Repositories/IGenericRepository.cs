using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GameManager.Domain.Contracts.Data.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity Get(string id);
        TEntity Get(string id, string[] includes);
        TEntity Get(string id, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Get(Func<TEntity, Boolean> predicate);
        IEnumerable<TEntity> Get(Func<TEntity, Boolean> predicate, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll();
        TEntity FirstOrDefault(Func<TEntity, Boolean> predicate);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Dispose();
    }
}

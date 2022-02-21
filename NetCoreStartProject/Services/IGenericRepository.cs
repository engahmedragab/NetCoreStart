using NetCoreStartProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCoreStartProject.Services
{
    public interface IGenericRepository<TEntity> where TEntity : Base
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        TEntity Delete(TEntity entity);
        TEntity HardDelete(TEntity entity);
        void HardDeleteRange(IEnumerable<TEntity> entity);
        bool Exists(int id);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        TEntity GetById(int id);
        TEntity Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entity);
        void Update(TEntity entity, TEntity entityUpdated);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        void Save();

        void DisconnectedUpdate(TEntity entity);
    }
}
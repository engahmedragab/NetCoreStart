    using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Linq.Expressions; 
using System.Data;
using System.Data.SqlClient;
using NetCoreStartProject.Data;
using NetCoreStartProject.Domain;
using NetCoreStartProject.Extensions;

namespace NetCoreStartProject.Services
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : Base
    {
        protected readonly DataContext _context;
        //protected readonly IDbConnection _dbConnection;

        public GenericRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //_dbConnection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity).Entity;
        }
        public TEntity Add(TEntity entity, string slugtitle)
        {
            entity.Slug = GenrateSlug(slugtitle);
            return _context.Set<TEntity>().Add(entity).Entity;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void DisconnectedUpdate(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            //_context.Set<TEntity>().Attach(entity);
            //_context.Entry(entity).State = EntityState.Modified;
            return _context.Set<TEntity>().Update(entity).Entity;
        }

        public void UpdateRange(ICollection<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
        }

        public void Update(TEntity entity, TEntity entityUpdated)
        {
            _context.Entry(entity).CurrentValues.SetValues(entityUpdated);
            

            //var entry = _context.Set<TEntity>().Entry(entity);
            //_dbSet.Attach(entity);
            //entry.CurrentValues.SetValues(entityUpdated);

        }

        public TEntity Delete(TEntity entity)
        {
            //return _context.Set<TEntity>().Remove(entity).Entity;
            entity.IsDeleted = true;
            return _context.Set<TEntity>().Update(entity).Entity;
        }

         

        public bool Exists(int id)
        {
            TEntity entity = _context.Set<TEntity>().FirstOrDefault(t=> t.Id == id && !t.IsDeleted);

            if (entity == null)
                return false;
            else
                return true;
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? _context.Set<TEntity>() : _context.Set<TEntity>().Where(predicate);
        }


        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        
        public TEntity GetByGuid(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    

        public void Dispose()
        {
            _context.Dispose();
        }

        public TEntity HardDelete(TEntity entity)
        {
            return _context.Set<TEntity>().Remove(entity).Entity;
        }

        public void HardDeleteRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Any(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private string GenrateSlug(string title)
        {
            var slug = title.GenrateSlug();
            while (!_context.Set<TEntity>().Any(x => x.Slug == slug))
            {
                slug = title.GenrateSlug();
            }
            return slug;
        }


    }
}

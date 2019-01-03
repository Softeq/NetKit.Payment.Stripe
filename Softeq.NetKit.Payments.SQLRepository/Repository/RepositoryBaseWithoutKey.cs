// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EnsureThat;
using Microsoft.EntityFrameworkCore;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public abstract class RepositoryBaseWithoutKey<TEntity> : Interfaces.IRepositoryWithoutKey<TEntity>
        where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryBaseWithoutKey(ApplicationDbContext dataContext)
        {
            EnsureArg.IsNotNull(dataContext, nameof(dataContext));

            DataContext = dataContext;
            DbSet = DataContext.Set<TEntity>();
        }

        protected ApplicationDbContext DataContext { get; }

        public virtual TEntity Add(TEntity entity)
        {
            EnsureArg.IsNotNull(entity, nameof(entity));
            DbSet.Add(entity);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            EnsureArg.IsNotNull(entity, nameof(entity));
            DbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            EnsureArg.IsNotNull(where, nameof(where));
            IEnumerable<TEntity> objects = DbSet.Where(where).AsEnumerable();
            foreach (TEntity obj in objects)
            {
                DbSet.Remove(obj);
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            EnsureArg.IsNotNull(where, nameof(where));
            return DbSet.Where(where);
        }

        public virtual void Update(TEntity entity)
        {
            EnsureArg.IsNotNull(entity, nameof(entity));
            DbSet.Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
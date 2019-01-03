// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Linq;
using System.Linq.Expressions;
using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IRepository<TEntity, in TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        TEntity GetById(TKey id);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        void Update(TEntity entity);
    }

    public interface IRepositoryWithoutKey<TEntity>
        where TEntity : class
    {
        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where);

        void Update(TEntity entity);
    }
}
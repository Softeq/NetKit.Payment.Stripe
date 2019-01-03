// Developed by Softeq Development Corporation
// http://www.softeq.com

using Softeq.NetKit.Payments.Data.Abstractions;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public abstract class RepositoryBase<TEntity, TKey> : RepositoryBaseWithoutKey<TEntity>, Interfaces.IRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>, new()
    {
        protected RepositoryBase(ApplicationDbContext context)
            : base(context)
        {
        }

        public virtual TEntity GetById(TKey id)
        {
            return DbSet.Find(id);
        }
    }
}
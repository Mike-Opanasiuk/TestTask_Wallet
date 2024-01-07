using Wallet.Core.Entities.Abstract;
using Wallet.Infrastructure.Repository.Abstract;

namespace Wallet.Infrastructure.Repository;


public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected AppDbContext context { get; }

    public Repository(AppDbContext context)
    {
        this.context = context;
    }

    public virtual async Task<TEntity> FindAsync(Guid id)
    {
        return await context.FindAsync<TEntity>(id);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        return (await context.Set<TEntity>().AddAsync(entity)).Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        return context.Update(entity).Entity;
    }

    public virtual void Delete(TEntity entity)
    {
        context.Remove(entity);
    }

    public IQueryable<TEntity> Get()
    {
        return context.Set<TEntity>();
    }
}


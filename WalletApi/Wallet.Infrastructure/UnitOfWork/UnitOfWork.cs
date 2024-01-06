using Wallet.Infrastructure.Repository.Abstract;
using Wallet.Infrastructure.Repository;
using Wallet.Infrastructure.UnitOfWork.Abstract;
using Wallet.Core.Entities;

namespace Wallet.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext context;

    public IRepository<CardEntity> Cards { get; }

    public UnitOfWork(AppDbContext context)
    {
        this.context = context;

        Cards = new Repository<CardEntity>(context);
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}

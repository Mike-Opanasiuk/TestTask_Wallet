using Wallet.Core.Entities;
using Wallet.Infrastructure.Repository.Abstract;

namespace Wallet.Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    IRepository<CardEntity> Cards { get; }
    IRepository<PointEntity> Points { get; }

    Task<int> SaveChangesAsync();
}
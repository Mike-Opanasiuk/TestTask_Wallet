using Wallet.Core.Entities;
using Wallet.Infrastructure.Repository.Abstract;

namespace Wallet.Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    IRepository<CardEntity> Cards { get; }

    Task<int> SaveChangesAsync();
}
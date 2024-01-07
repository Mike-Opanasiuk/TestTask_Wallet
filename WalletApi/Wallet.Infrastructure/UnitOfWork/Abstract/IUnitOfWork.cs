using Wallet.Core.Entities;
using Wallet.Infrastructure.Repository.Abstract;

namespace Wallet.Infrastructure.UnitOfWork.Abstract;

public interface IUnitOfWork
{
    IRepository<CardEntity> Cards { get; }
    IRepository<PointEntity> Points { get; }
    IRepository<TransactionEntity> Transactions { get; }
    IRepository<TransactionTypeEntity> TransactionTypes { get; }
    IRepository<TransactionStatusEntity> TransactionStatuses { get; }
    IRepository<TransactionCategoryEntity> TransactionCategories { get; }

    Task<int> SaveChangesAsync();
}
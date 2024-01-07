
using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("TransactionStatuses")]
public class TransactionStatusEntity : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}

using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("TransactionTypes")]
public class TransactionTypeEntity : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}

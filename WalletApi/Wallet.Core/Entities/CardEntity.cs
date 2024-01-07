using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("Cards")]
public class CardEntity : BaseEntity
{
    public decimal MaxLimit { get; set; }
    public decimal Balance { get; set; }

    [NotMapped]
    public decimal Available => MaxLimit - Balance;

    public Guid OwnerId { get; set; }

    public required UserEntity Owner { get; set; }

    public ICollection<PointEntity> Points { get; set; } = new List<PointEntity>();

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}

using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("Points")]
public class PointEntity : BaseEntity
{
    public decimal Amount { get; set; }

    public Guid CardId { get; set; }

    public CardEntity? Card { get; set; }
}

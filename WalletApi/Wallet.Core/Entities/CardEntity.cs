using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

public class CardEntity : BaseEntity
{
    public decimal MaxLimit { get; set; }
    public decimal Balance { get; set; }

    [NotMapped]
    public decimal Available => MaxLimit - Balance;

    public Guid OwnerId { get; set; }

    public UserEntity Owner { get; set; }
}

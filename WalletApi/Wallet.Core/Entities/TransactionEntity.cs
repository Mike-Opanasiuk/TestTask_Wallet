using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("Transactions")]
public class TransactionEntity : BaseEntity
{
    public decimal Sum { get; set; }

    public string Description { get; set; }

    public Guid? AuthorizedUserId { get; set; }
    public UserEntity? AuthorizedUser { get; set; }

    public Guid CardId { get; set; }

    public CardEntity Card { get; set; }

    public Guid TypeId { get; set; }
    public TransactionTypeEntity Type { get; set; }

    public Guid CategoryId { get; set; }
    public TransactionCategoryEntity Category { get; set; }

    public Guid StatusId { get; set; }
    public TransactionStatusEntity Status { get; set; }
}

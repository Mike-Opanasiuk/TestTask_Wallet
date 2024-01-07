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

    public Guid TransactionTypeId { get; set; }
    public TransactionTypeEntity TransactionType { get; set; }

    public Guid TransactionCategoryId { get; set; }
    public TransactionCategoryEntity TransactionCategory { get; set; }

    public Guid TransactionStatusId { get; set; }
    public TransactionStatusEntity TransactionStatus { get; set; }
}

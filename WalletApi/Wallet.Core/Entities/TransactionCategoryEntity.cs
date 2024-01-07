using System.ComponentModel.DataAnnotations.Schema;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

[Table("TransactionCategories")]
public class TransactionCategoryEntity : BaseEntity
{
    public required string Name { get; set; }
    public string? BackgroundImage { get; set; }
    public string? Icon { get; set; }

    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}

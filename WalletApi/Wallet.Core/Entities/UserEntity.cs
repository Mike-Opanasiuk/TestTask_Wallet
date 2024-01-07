using Microsoft.AspNetCore.Identity;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string? Name { get; set; }

    public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>(); 

    // transaction where this user is 'AuthoredUser'
    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}
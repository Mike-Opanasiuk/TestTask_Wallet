using Microsoft.AspNetCore.Identity;
using Wallet.Core.Entities.Abstract;

namespace Wallet.Core.Entities;

public class RoleEntity : IdentityRole<Guid>, IEntity
{
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
namespace Wallet.Core.Entities.Abstract;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}
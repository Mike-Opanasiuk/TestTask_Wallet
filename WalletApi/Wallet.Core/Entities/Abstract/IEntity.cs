namespace Wallet.Core.Entities.Abstract;

public interface IEntity
{
    Guid Id { get; set; }

    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn { get; set; }
}
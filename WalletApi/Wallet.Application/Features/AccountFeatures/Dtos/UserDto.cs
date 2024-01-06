namespace Wallet.Application.Features.AccountFeatures.Dtos;

public record UserDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string UserName { get; set; }
}

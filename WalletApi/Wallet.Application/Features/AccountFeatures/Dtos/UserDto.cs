namespace Wallet.Application.Features.AccountFeatures.Dtos;

public record UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
}

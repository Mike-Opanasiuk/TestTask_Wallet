using Microsoft.AspNetCore.Identity;

namespace Wallet.Shared.CustomExceptions;

public class BadRequestException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; }

    public BadRequestException(string message, IEnumerable<IdentityError>? errors = null)
        : base(message) => Errors = errors;
}

namespace Wallet.Shared;

public class AppConstant
{
    public record Claims
    {
        public const string Id = "id";
        public const string Roles = "roles";
    }

    public record Roles
    {
        public const string User = "User";
    }

    public record JwtTokenLifetimes
    {
        public static readonly TimeSpan Default = TimeSpan.FromHours(12);
    }
}

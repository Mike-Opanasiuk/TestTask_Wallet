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

    public record FireBase
    {
        public const string Url = "wallet-3018e.appspot.com";
    }

    public record Random
    {
        public const string BigImgUrl = "https://random.imagecdn.app/250/250";
        public const string SmallImgUrl = "https://random.imagecdn.app/50/50";
    }

    public record CardLimits
    {
        public const decimal Default = 1500;
    }

    public record Jobs
    {
        // cron expression to run job every day at 4 am
        public static readonly string Frequency = @"0 4 * * *";

        // job name to add points
        public static readonly string AddPointsJobName = "AddPointsJob";
    }

    public record TransactionStatuses
    {
        public static KeyValuePair<Guid, string> Pending =>
            new KeyValuePair<Guid, string>(Guid.Parse("6C45F007-AE6B-4684-9AD4-59CB8B8BB38B"), "Pending");
        public static KeyValuePair<Guid, string> Approved =>
            new KeyValuePair<Guid, string>(Guid.Parse("2CAF2A4F-3CD9-47C6-BFA0-B706A4E113C9"), "Approved");
        public static KeyValuePair<Guid, string> Canceled =>
            new KeyValuePair<Guid, string>(Guid.Parse("FA6B2E6B-CA26-40A7-B239-050CEAD3F32A"), "Canceled");
    }

    public record TransactionTypes
    {
        public static KeyValuePair<Guid, string> Payment =>
            new KeyValuePair<Guid, string>(Guid.Parse("E4C50F39-577B-4D68-AD2A-78B27B3B0563"), "Payment");

        public static KeyValuePair<Guid, string> Credit =>
            new KeyValuePair<Guid, string>(Guid.Parse("FB037D63-F712-465E-AA6A-E3B44F5CF29B"), "Credit");
    }

    public record TransactionCategories
    {
        public static KeyValuePair<Guid, string> Target =>
            new KeyValuePair<Guid, string>(Guid.Parse("F4827760-69A0-48C6-8DDC-46301A7099A9"), "Target");

        public static KeyValuePair<Guid, string> IKEA =>
            new KeyValuePair<Guid, string>(Guid.Parse("5F193AB4-79DC-4F1B-846A-E8B2A9C9FA7F"), "IKEA");

        public static KeyValuePair<Guid, string> Apple =>
            new KeyValuePair<Guid, string>(Guid.Parse("FFD31FA5-CE7F-401F-B3A3-57FE89F2261E"), "Apple");

        public static KeyValuePair<Guid, string> Other =>
            new KeyValuePair<Guid, string>(Guid.Parse("049DF687-EF15-40DD-AD12-D9C6934EAC09"), "Other");
    }
}

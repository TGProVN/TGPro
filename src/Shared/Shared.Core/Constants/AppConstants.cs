namespace Shared.Core.Constants;

public static class AppConstants
{
    public static class Messages
    {
        public const string ValidationError = "One or more validation failures have occurred!";
        public const string InvalidCredentialInfo = "Email or password is incorrect.";
        public const string EmailUnconfirmed = "Your Email Address is not confirmed.";
        public const string LockedUser =
            "Your account has been locked! Please contact your website administrator for more information.";
        public const string MailSent = "An email has been sent. Please check your mailbox and follow the instructions.";
        public const string Unauthorized = "Unauthorized!";
        public const string Forbidden = "Forbidden!";
        public const string InternalServerError = "An unhandled error has occurred.";
    }

    public static class Roles
    {
        public const string Administrator = "Administrator";
        public const string Moderator = "Moderator";
        public const string Basic = "Basic";
    }

    public static class TableSchemas
    {
        public const string Identity = "Identity";
        public const string Catalog = "Catalog";
    }

    public static class ClaimTypes
    {
        public const string Permission = "Permission";
    }

    public static class Entities
    {
        public const string User = "users";
        public const string Brand = "brands";
    }

    public static readonly string[] GoogleScopes = {
        "https://www.googleapis.com/auth/userinfo.profile",
        "https://www.googleapis.com/auth/userinfo.email"
    };
}

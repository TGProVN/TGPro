namespace Shared.Core.Constants;

public static class AppConstants
{

    public static readonly string[] GoogleScopes = {
        "https://www.googleapis.com/auth/userinfo.profile",
        "https://www.googleapis.com/auth/userinfo.email"
    };

    public static class DefaultImages
    {
        public const string FemaleAvatar =
            "https://res.cloudinary.com/tgproimagecloud/image/upload/v1638705521/TGProV3/users/default/default_female_photo.jpg";

        public const string FemaleAvatarId = "TGProV3/users/default/default_female_photo";

        public const string MaleAvatar =
            "https://res.cloudinary.com/tgproimagecloud/image/upload/v1638705521/TGProV3/users/default/default_male_photo.jpg";

        public const string MaleAvatarId = "TGProV3/users/default/default_male_photo";

        public const string BrandImage =
            "https://res.cloudinary.com/tgproimagecloud/image/upload/v1638721852/TGProV3/brands/default/default_brand_photo.jpg";

        public const string BrandImageId = "TGProV3/brands/default/default_brand_photo";

        public const string ProductImage =
            "https://res.cloudinary.com/tgproimagecloud/image/upload/v1638867804/TGProV3/products/default/default_product_photo.jpg";

        public const string ProductImageId = "TGProV3/products/default/default_product_photo";
    }

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
        public const string System = "System";
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

    public static class StatusCode
    {
        // Informational 1xx
        public const int Continue = 100;
        public const int SwitchingProtocols = 101;
        public const int Processing = 102;
        public const int EarlyHints = 103;

        // Successful 2xx
        public const int Ok = 200;
        public const int Created = 201;
        public const int Accepted = 202;
        public const int NoContent = 204;
        public const int PartialContent = 206;

        // Redirection 3xx
        public const int MultipleChoices = 300;
        public const int Ambiguous = 300;
        public const int MovedPermanently = 301;
        public const int Moved = 301;
        public const int Found = 302;
        public const int Redirect = 302;
        public const int SeeOther = 303;
        public const int RedirectMethod = 303;
        public const int NotModified = 304;
        public const int UseProxy = 305;
        public const int Unused = 306;
        public const int TemporaryRedirect = 307;
        public const int RedirectKeepVerb = 307;
        public const int PermanentRedirect = 308;

        // Client Error 4xx
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int PaymentRequired = 402;
        public const int Forbidden = 403;
        public const int NotFound = 404;
        public const int MethodNotAllowed = 405;
        public const int NotAcceptable = 406;
        public const int ProxyAuthenticationRequired = 407;
        public const int RequestTimeout = 408;
        public const int Conflict = 409;
        public const int Gone = 410;
        public const int LengthRequired = 411;
        public const int PreconditionFailed = 412;
        public const int RequestEntityTooLarge = 413;
        public const int RequestUriTooLong = 414;
        public const int UnsupportedMediaType = 415;
        public const int RequestedRangeNotSatisfiable = 416;

        public const int ExpectationFailed = 417;

        // From https://github.com/dotnet/runtime/issues/15650:
        // "It would be a mistake to add it to .NET now. See golang/go#21326,
        // nodejs/node#14644, requests/requests#4238 and aspnet/HttpAbstractions#915".
        // ImATeapot = 418
        public const int MisdirectedRequest = 421;
        public const int UnprocessableEntity = 422;
        public const int Locked = 423;
        public const int FailedDependency = 424;
        public const int UpgradeRequired = 426;
        public const int PreconditionRequired = 428;
        public const int TooManyRequests = 429;
        public const int RequestHeaderFieldsTooLarge = 431;
        public const int UnavailableForLegalReasons = 451;

        // Server Error 5xx
        public const int InternalServerError = 500;
        public const int NotImplemented = 501;
        public const int BadGateway = 502;
        public const int ServiceUnavailable = 503;
        public const int GatewayTimeout = 504;
        public const int HttpVersionNotSupported = 505;
        public const int VariantAlsoNegotiates = 506;
        public const int InsufficientStorage = 507;
        public const int LoopDetected = 508;
        public const int NotExtended = 510;
        public const int NetworkAuthenticationRequired = 511;
    }
}

using System.ComponentModel;

namespace Shared.Core.Constants;

public static class AppPermissions
{
    [DisplayName("Brands")]
    [Description("Brands Permissions")]
    public static class Brands
    {
        public const string Create = "Permissions.Brands.Create";
        public const string Read = "Permissions.Brands.Read";
        public const string Update = "Permissions.Brands.Update";
        public const string Delete = "Permissions.Brands.Delete";
        public const string Export = "Permissions.Brands.Export";
        public const string Retrieve = "Permissions.Brands.Retrieve";
    }

    [DisplayName("Products")]
    [Description("Products Permissions")]
    public static class Products
    {
        public const string Create = "Permissions.Products.Create";
        public const string Read = "Permissions.Products.Read";
        public const string Update = "Permissions.Products.Update";
        public const string Delete = "Permissions.Products.Delete";
        public const string Export = "Permissions.Products.Export";
        public const string Retrieve = "Permissions.Products.Retrieve";
    }

    [DisplayName("Users")]
    [Description("Users Permissions")]
    public static class Users
    {
        public const string Create = "Permissions.Users.Create";
        public const string Read = "Permissions.Users.Read";
        public const string Update = "Permissions.Users.Update";
        public const string FullUpdate = "Permissions.Users.Full-Update";
        public const string Delete = "Permissions.Users.Delete";
        public const string Export = "Permissions.Users.Export";
        public const string Retrieve = "Permissions.Users.Retrieve";
    }

    [DisplayName("Internal")]
    [Description("Roles Permissions")]
    public static class Roles
    {
        public const string Create = "Permissions.Internal.Roles.Create";
        public const string Read = "Permissions.Internal.Roles.Read";
        public const string Update = "Permissions.Internal.Roles.Update";
        public const string Delete = "Permissions.Internal.Roles.Delete";
        public const string Export = "Permissions.Internal.Roles.Export";
        public const string Retrieve = "Permissions.Internal.Roles.Retrieve";
    }

    [DisplayName("Internal")]
    [Description("RoleClaims Permissions")]
    public static class RoleClaims
    {
        public const string Create = "Permissions.Internal.RoleClaims.Create";
        public const string Read = "Permissions.Internal.RoleClaims.Read";
        public const string Update = "Permissions.Internal.RoleClaims.Update";
        public const string Delete = "Permissions.Internal.RoleClaims.Delete";
        public const string Export = "Permissions.Internal.RoleClaims.Export";
        public const string Retrieve = "Permissions.Internal.RoleClaims.Retrieve";
    }

    [DisplayName("Internal")]
    [Description("Communication Permissions")]
    public static class Communications
    {
        public const string Chat = "Permissions.Internal.Communications.Chat";
        public const string Comment = "Permissions.Internal.Communications.Comment";
        public const string ReplyComment = "Permissions.Internal.Communications.ReplyComment";
    }

    [DisplayName("Internal")]
    [Description("Dashboards Permissions")]
    public static class Dashboards
    {
        public const string View = "Permissions.Internal.Dashboards.View";
    }

    [DisplayName("Internal")]
    [Description("Hangfire Permissions")]
    public static class Hangfires
    {
        public const string View = "Permissions.Internal.Hangfires.View";
    }

    [DisplayName("Internal")]
    [Description("AuditTrails Permissions")]
    public static class AuditTrails
    {
        public const string View = "Permissions.Internal.AuditTrails.View";
        public const string Export = "Permissions.Internal.AuditTrails.Export";
        public const string Search = "Permissions.Internal.AuditTrails.Search";
    }
}

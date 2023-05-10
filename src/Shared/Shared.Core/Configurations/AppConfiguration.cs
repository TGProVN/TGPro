namespace Shared.Core.Configurations;

public class AppConfiguration
{
    public required string Secret { get; set; }
    public required string DefaultLoginProvider { get; set; }
    public required string GoogleLoginProvider { get; set; }
    public required string DefaultAdminPassword { get; set; }
    public required string DefaultSystemWidePassword { get; set; }
    public required string DefaultAppPassword { get; set; }
    public required string DefaultDbPassword { get; set; }
    public required string ApplicationUrl { get; set; }
    public required int ApiMajorVersion { get; set; }
    public required int ApiMinorVersion { get; set; }
    public required string ApiVersionGroupNameFormat { get; set; }
}

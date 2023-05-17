namespace Shared.Core.Structs;

public record struct PermissionDetail(
    string Group,
    string Description,
    IEnumerable<string?> Permissions
);

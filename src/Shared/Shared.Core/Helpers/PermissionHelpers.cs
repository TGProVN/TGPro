using Shared.Core.Constants;
using Shared.Core.Extensions;

namespace Shared.Core.Helpers;

public static class PermissionHelpers
{
    public static IEnumerable<Tuple<string, string, IEnumerable<string?>>> GetAllAppPermissions()
    {
        var permissionsInfo = new List<Tuple<string, string, IEnumerable<string?>>>();
        var displayNameAttributes = typeof(AppPermissions).GetDisplayNameAttributes();
        var descriptionAttributes = typeof(AppPermissions).GetDescriptionAttributes();

        for (var i = 0; i < displayNameAttributes.Length; i++)
        {
            var displayName = displayNameAttributes[i].DisplayName;
            var description = descriptionAttributes[i].Description;
            var permissions = typeof(AppPermissions)
                             .GetNestedTypes()
                             .GetFieldInfo(value => !string.IsNullOrEmpty(value) &&
                                                    value.Contains(displayName) &&
                                                    value.Contains(description.Split(" ")[0]));

            permissionsInfo.Add(new Tuple<string, string, IEnumerable<string?>>(displayName, description, permissions));
        }

        return permissionsInfo;
    }

    public static IEnumerable<Tuple<string, string, IEnumerable<string?>>> GetDefaultAppPermissions()
    {
        var permissionsInfo = new List<Tuple<string, string, IEnumerable<string?>>>();
        var displayNameAttributes = typeof(AppPermissions).GetDisplayNameAttributes();
        var descriptionAttributes = typeof(AppPermissions).GetDescriptionAttributes();

        for (var i = 0; i < displayNameAttributes.Length; i++)
        {
            var displayName = displayNameAttributes[i].DisplayName;
            var description = descriptionAttributes[i].Description;
            var permissions = typeof(AppPermissions)
                             .GetNestedTypes()
                             .GetFieldInfo(value => !string.IsNullOrEmpty(value) &&
                                                    !value.Contains(".Internal.") &&
                                                    (value.Contains(".Read") || value.Contains(".Retrieve")) &&
                                                    value.Contains(description.Split(" ")[0]));

            permissionsInfo.Add(new Tuple<string, string, IEnumerable<string?>>(displayName, description, permissions));
        }

        return permissionsInfo;
    }
}

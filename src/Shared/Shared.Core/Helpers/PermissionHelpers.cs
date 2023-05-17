using Shared.Core.Constants;
using Shared.Core.Extensions;
using Shared.Core.Structs;

namespace Shared.Core.Helpers;

public static class PermissionHelpers
{
    public static IEnumerable<PermissionDetail> GetAllAppPermissions()
    {
        var result = new List<PermissionDetail>();
        var displayNameAttributes = typeof(AppPermissions).GetDisplayNameAttributes();
        var descriptionAttributes = typeof(AppPermissions).GetDescriptionAttributes();

        for (var i = 0; i < displayNameAttributes.Length; i++)
        {
            var group = displayNameAttributes[i].DisplayName;
            var description = descriptionAttributes[i].Description;
            var permissions = typeof(AppPermissions)
                             .GetNestedTypes()
                             .GetFieldInfo(value => !string.IsNullOrEmpty(value) &&
                                                    value.Contains(group) &&
                                                    value.Contains(description.Split(" ")[0]));

            result.Add(new PermissionDetail(group, description, permissions));
        }

        return result;
    }

    public static IEnumerable<PermissionDetail> GetDefaultAppPermissions()
    {
        var result = new List<PermissionDetail>();
        var displayNameAttributes = typeof(AppPermissions).GetDisplayNameAttributes();
        var descriptionAttributes = typeof(AppPermissions).GetDescriptionAttributes();

        for (var i = 0; i < displayNameAttributes.Length; i++)
        {
            var group = displayNameAttributes[i].DisplayName;
            var description = descriptionAttributes[i].Description;
            var permissions = typeof(AppPermissions)
                             .GetNestedTypes()
                             .GetFieldInfo(value => !string.IsNullOrEmpty(value) &&
                                                    !value.Contains(".Internal.") &&
                                                    (value.Contains(".Read") || value.Contains(".Retrieve")) &&
                                                    value.Contains(description.Split(" ")[0]));

            result.Add(new PermissionDetail(group, description, permissions));
        }

        return result;
    }
}

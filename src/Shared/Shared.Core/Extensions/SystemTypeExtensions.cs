using System.ComponentModel;
using System.Reflection;

namespace Shared.Core.Extensions;

public static class SystemTypeExtensions
{
    public static DisplayNameAttribute[] GetDisplayNameAttributes(this Type type)
    {
        return type.GetNestedTypes().GetCustomAttributes<DisplayNameAttribute>();
    }

    public static DescriptionAttribute[] GetDescriptionAttributes(this Type type)
    {
        return type.GetNestedTypes().GetCustomAttributes<DescriptionAttribute>();
    }

    private static T[] GetCustomAttributes<T>(this IEnumerable<Type> types) where T : Attribute
    {
        return types.Select(type => (T) type.GetCustomAttribute(typeof(T), false)!).ToArray();
    }

    public static IEnumerable<string?> GetFieldInfo(this IEnumerable<Type> types, Func<string?, bool> predicate)
    {
        return types.SelectMany(
                         type => type.GetFields(BindingFlags.Public |
                                                BindingFlags.Static |
                                                BindingFlags.FlattenHierarchy)
                     )
                    .Select(fieldInfo => fieldInfo.GetValue(null)?.ToString())
                    .Where(predicate)
                    .ToList();
    }
}

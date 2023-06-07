namespace AdminPortal.Extensions;

public static class ParseValue
{
    public static int ToIntValue<T>(this T value)
    {
        return typeof(T) == typeof(int) ? Convert.ToInt32(value) : default;
    }

    public static string ToStringValue<T>(this T value)
    {
        return typeof(T) == typeof(string) ? Convert.ToString(value) ?? "0" : "0";
    }
}

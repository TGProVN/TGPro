using MudBlazor;

namespace AdminPortal.Extensions;

public static class ThemeExtensions
{
    public static void ChangeFont(this MudTheme theme, string font)
    {
        var value = new[] { font, "Helvetica", "Arial", "sans-serif" };

        theme.Typography.Default.FontFamily = value;
        theme.Typography.H1.FontFamily = value;
        theme.Typography.H2.FontFamily = value;
        theme.Typography.H3.FontFamily = value;
        theme.Typography.H4.FontFamily = value;
        theme.Typography.H5.FontFamily = value;
        theme.Typography.H6.FontFamily = value;
        theme.Typography.Button.FontFamily = value;
        theme.Typography.Body1.FontFamily = value;
        theme.Typography.Body2.FontFamily = value;
        theme.Typography.Caption.FontFamily = value;
        theme.Typography.Subtitle1.FontFamily = value;
        theme.Typography.Subtitle2.FontFamily = value;
    }
}

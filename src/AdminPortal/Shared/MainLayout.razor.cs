using AdminPortal.Enums;
using AdminPortal.Extensions;
using AdminPortal.Models;
using MudBlazor;

namespace AdminPortal.Shared;

public partial class MainLayout
{
    private bool _isOpenSetting;
    private bool _isDrawerOpen = true;
    private bool _isFirstTimeEntry = true;
    private bool _isInitialized;

    private readonly IEnumerable<string> _lstFont = new[]
        { "Roboto", "Open Sans", "Lato", "Poppins", "Ubuntu", "Oxygen" };

    private MudThemeProvider _themeProvider = null!;
    private readonly UserPreference _userPreference = new();
    private readonly MudTheme _theme = new Theme();

    private readonly List<BreadcrumbItem> _items = new() {
        new BreadcrumbItem("Personal", "#"),
        new BreadcrumbItem("Dashboard", "#")
    };

    protected override async Task OnInitializedAsync()
    {
        var currentUserPreference = await UserPreferenceService.GetUserPreference();

        if (currentUserPreference is not null)
        {
            _isFirstTimeEntry = false;
            _userPreference.AppBarElevation = currentUserPreference.AppBarElevation;
            _userPreference.BorderRadius = currentUserPreference.BorderRadius;
            _userPreference.DarkMode = currentUserPreference.DarkMode;
            _userPreference.DrawerClipMode = currentUserPreference.DrawerClipMode;
            _userPreference.DrawerElevation = currentUserPreference.DrawerElevation;
            _userPreference.Font = currentUserPreference.Font;
        }

        _theme.ChangeFont(_userPreference.Font);
        _isInitialized = true;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (_isInitialized && _isFirstTimeEntry)
            {
                await SetThemeAsSystemPreference();
            }

            await _themeProvider.WatchSystemPreference(OnSystemPreferenceChanged);
            StateHasChanged();
        }

        if (_isInitialized && _isFirstTimeEntry)
        {
            await SetThemeAsSystemPreference();
            StateHasChanged();
        }
    }

    private async Task SetThemeAsSystemPreference()
    {
        _isFirstTimeEntry = !_isFirstTimeEntry;
        _userPreference.DarkMode = await _themeProvider.GetSystemPreference();
        await UserPreferenceService.SetUserPreference(_userPreference);
    }

    private async Task OnSystemPreferenceChanged(bool newValue)
    {
        _userPreference.DarkMode = newValue;
        await UserPreferenceService.SetUserPreference(_userPreference);
        StateHasChanged();
    }

    private void OnSettingToggle()
    {
        _isOpenSetting = !_isOpenSetting;
    }

    private void OnDrawerToggle()
    {
        _isDrawerOpen = !_isDrawerOpen;
    }

    private async Task OnThemeModeChange(bool isDark)
    {
        _userPreference.DarkMode = isDark;
        await UserPreferenceService.SetUserPreference(_userPreference);
    }

    private async Task OnLayoutChange<T>(T value, LayoutProperty layoutProperty)
    {
        switch (layoutProperty)
        {
            case LayoutProperty.AppBarElevation:
                _userPreference.AppBarElevation = value.ToIntValue();
                break;
            case LayoutProperty.BorderRadius:
                _userPreference.BorderRadius = value.ToIntValue();
                _theme.LayoutProperties.DefaultBorderRadius = $"{value.ToIntValue()}px";
                break;
            case LayoutProperty.DrawerClipMode:
                _userPreference.DrawerClipMode = value switch {
                    "Always" => DrawerClipMode.Always,
                    "Docked" => DrawerClipMode.Docked,
                    _ => DrawerClipMode.Never
                };
                break;
            case LayoutProperty.DrawerElevation:
                _userPreference.DrawerElevation = value.ToIntValue();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(layoutProperty), layoutProperty, null);
        }

        await UserPreferenceService.SetUserPreference(_userPreference);
    }

    private async Task OnFontChange(string value)
    {
        _theme.ChangeFont(value);
        _userPreference.Font = value;
        await UserPreferenceService.SetUserPreference(_userPreference);
    }
}

using Microsoft.AspNetCore.Components;

namespace AdminPortal.Shared;

public partial class SettingLayout
{
    private string MudExpansionPanelStyle { get; set; } = "background: var(--mud-palette-drawer-background)";

    [Parameter]
    public bool Open { get; set; }

    [Parameter]
    public EventCallback OnOpenSettingClick { get; set; }

    [Parameter]
    public EventCallback<bool> OnChangeThemeModeClick { get; set; }

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private bool _openSection;

    private async Task OnThemeChange(bool isDark)
    {
        await OnChangeThemeModeClick.InvokeAsync(isDark);
    }
}

using MudBlazor;

namespace AdminPortal.Models;

public class UserPreference
{
    public int AppBarElevation { get; set; }
    public int BorderRadius { get; set; }
    public bool DarkMode { get; set; }
    public DrawerClipMode DrawerClipMode { get; set; }
    public int DrawerElevation { get; set; }
    public string Font { get; set; }

    public UserPreference()
    {
        DrawerClipMode = DrawerClipMode.Always;
        BorderRadius = 4;
        AppBarElevation = 4;
        DrawerElevation = 24;
        Font = "Roboto";
    }
}

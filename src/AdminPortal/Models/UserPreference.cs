using MudBlazor;

namespace AdminPortal.Models;

public class UserPreference
{
    public bool DarkMode { get; set; }
    public DrawerClipMode DrawerClipMode { get; set; }
    public int BorderRadius { get; set; }
    public int AppBarElevation { get; set; }
    public int DrawerElevation { get; set; }

    public UserPreference()
    {
        DarkMode = false;
        DrawerClipMode = DrawerClipMode.Always;
        BorderRadius = 4;
        AppBarElevation = 4;
        DrawerElevation = 25;
    }
}

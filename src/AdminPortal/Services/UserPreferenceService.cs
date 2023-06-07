using AdminPortal.Models;
using Blazored.LocalStorage;
using System.Text.Json;

namespace AdminPortal.Services;

public class UserPreferenceService
{
    private readonly ILocalStorageService _localStorage;

    public UserPreferenceService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<UserPreference?> GetUserPreference()
    {
        var settingValue = await _localStorage.GetItemAsStringAsync("userPreferences");

        return settingValue is not null ? JsonSerializer.Deserialize<UserPreference>(settingValue) : null;
    }

    public async Task SetUserPreference(UserPreference userPreference)
    {
        var settingValue = JsonSerializer.Serialize(userPreference);

        await _localStorage.SetItemAsStringAsync("userPreferences", settingValue);
    }
}

namespace Lazy.MyPhotos.App.Infrastructure.Services.Impl;

public class SettingsService : ISettingsService
{
    private const string AccessToken = "access_token";
    private const string AccessTokenDefault = "";

    public string AuthAccessToken
    {
        get => Preferences.Get(AccessToken, AccessTokenDefault);
        set => Preferences.Set(AccessToken, value);
    }

    public void ClearAll() => Preferences.Clear();
}
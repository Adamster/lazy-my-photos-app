using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Shared.Models.User;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Impl;

public class SettingsService : ISettingsService
{
    private const string AccessTokenKey = "access_token";
    private const string RefreshTokenKey = "refresh_token";
    private const string AccessTokenDefault = "";
    private const string RefreshTokenDefault = "";

    public string AuthAccessToken
    {
        get => Preferences.Get(AccessTokenKey, AccessTokenDefault);
        set => Preferences.Set(AccessTokenKey, value);
    }

    public string RefreshToken
    {
        get => Preferences.Get(RefreshTokenKey, RefreshTokenDefault);
        set => Preferences.Set(RefreshTokenKey, value);
    }

    public void ClearAll()
    {
        Preferences.Clear();
        //logger.LogInformation("All Settings cleared");
    }

    public void SaveLoginResponse(LoginResponse refreshResultContent)
    {
      //  logger.LogInformation("Saving login response");
        AuthAccessToken = refreshResultContent.AccessToken;
        RefreshToken = refreshResultContent.RefreshToken;
      //  logger.LogInformation("Access and Refresh token saved");
    }
}
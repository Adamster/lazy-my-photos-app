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

    private readonly ILogger<SettingsService> _logger;

    public SettingsService(ILogger<SettingsService> logger)
    {
        _logger = logger;
        Task.Run(LoadValues);
    }

    private async Task LoadValues()
    {
        AuthAccessToken = await SecureStorage.Default.GetAsync(AccessTokenKey) ?? AccessTokenDefault;
        RefreshToken = await SecureStorage.Default.GetAsync(RefreshTokenKey) ?? RefreshTokenDefault;
    }


    public string AuthAccessToken { get; private set; } = null!;

    public string RefreshToken { get; private set; } = null!;

    public void ClearAll()
    {
        SecureStorage.Default.RemoveAll();
        Preferences.Clear();
        _logger.LogInformation("All Settings cleared");
    }

    public async Task SaveLoginResponse(LoginResponse loginResponse)
    {
        _logger.LogInformation("Saving login response");

        await SecureStorage.Default.SetAsync(AccessTokenKey, loginResponse.AccessToken);
        await SecureStorage.Default.SetAsync(RefreshTokenKey, loginResponse.RefreshToken);

        _logger.LogInformation("Access and Refresh token saved");
    }
}
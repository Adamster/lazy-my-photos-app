using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;

public interface ISettingsService
{
    string AuthAccessToken { get; set; }
    string RefreshToken { get; set; }

    void ClearAll();
    
    void SaveLoginResponse(LoginResponse refreshResultContent);
}
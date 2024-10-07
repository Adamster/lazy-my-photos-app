using Lazy.MyPhotos.Shared.Models.User;

namespace Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;

public interface ISettingsService
{
    string AuthAccessToken { get; }
    string RefreshToken { get;  }

    void ClearAll();
    
    Task SaveLoginResponse(LoginResponse loginResponse);
}
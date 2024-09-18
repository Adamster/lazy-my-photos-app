namespace Lazy.MyPhotos.App.Infrastructure.Services;

public interface ISettingsService
{
    string AuthAccessToken { get; set; }

    void ClearAll();
}
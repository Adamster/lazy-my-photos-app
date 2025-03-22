using Lazy.MyPhotos.App.Infrastructure.Services.Impl;
using Lazy.MyPhotos.App.Infrastructure.Services.Interfaces;
using Lazy.MyPhotos.Shared.Services.Interfaces;

namespace Lazy.MyPhotos.App.Infrastructure.Services;

public static class ServicesContainerExtensions
{
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<IUserService, UserService>();
        mauiAppBuilder.Services.AddSingleton<IPhotoScanService, PhotoScanService>();

        return mauiAppBuilder;
    }
}
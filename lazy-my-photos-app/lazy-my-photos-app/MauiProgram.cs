using CommunityToolkit.Maui;
using Lazy.MyPhotos.App.Extensions;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Services;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Services;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lazy.MyPhotos.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterViews()
            .RegisterViewModels()
            .RegisterApiServices()
            .RegisterServices()
            .RegisterRoutes();

        builder.Services.AddSingleton<IRequestPhotoAccessPermissionHandler, RequestPhotoAccessPermissionHandler>();
#if ANDROID
        builder.Services.AddSingleton<IGalleryService, AndroidGalleryService>();
#elif IOS
         builder.Services.AddSingleton<IGalleryService, iOSGalleryService>();
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
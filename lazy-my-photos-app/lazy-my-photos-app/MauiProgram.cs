using CommunityToolkit.Maui;
using Lazy.MyPhotos.App.Extensions;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;

#if ANDROID
using Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;
using Lazy.MyPhotos.App.Platforms.Android.Handlers;
#endif

#if IOS
using Lazy.MyPhotos.App.Infrastructure.Platforms.iOS.Services;
#endif

using Lazy.MyPhotos.App.Infrastructure.Services;

#if !ANDROID
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl;
#endif
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;

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
#if ANDROID || IOS 
        builder.Services.AddSingleton<IGalleryService, GalleryService>();
#endif

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
using CommunityToolkit.Maui;
using Lazy.MyPhotos.App.Extensions;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;


#if ANDROID
using Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;
#endif

#if IOS
using Lazy.MyPhotos.App.Infrastructure.Platforms.iOS.Services;
#endif

#if WINDOWS
using Lazy.MyPhotos.App.Infrastructure.Platforms.Windows;
#endif

using Lazy.MyPhotos.App.Infrastructure.Services;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.Persistence;
using Lazy.MyPhotos.Shared.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;



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


        builder.Services.AddSingleton<PhotoDbContext>();
        builder.Services.AddSingleton<IRequestPhotoAccessPermissionHandler, RequestPhotoAccessPermissionHandler>();

        builder.Services.AddSingleton<IGalleryService, GalleryService>();
        

#if DEBUG
        builder.Logging.AddDebug();
#endif



        //TODO: publish a message to start indexing the gallery
        return builder.Build();
    }
}
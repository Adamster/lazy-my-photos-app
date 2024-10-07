using CommunityToolkit.Maui;
using Lazy.MyPhotos.App.Extensions;
using Lazy.MyPhotos.App.Infrastructure;
using Lazy.MyPhotos.App.Modules.Photo;
using Lazy.MyPhotos.Persistence;
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
            .RegisterInfrastructure()
            .RegisterPersistence()
            .RegisterRoutes()
            .RegisterPhotoModule();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
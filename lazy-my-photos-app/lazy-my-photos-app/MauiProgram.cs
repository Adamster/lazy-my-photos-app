using CommunityToolkit.Maui;
using Lazy.MyPhotos.App.Extensions;
using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Services;
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
            .RegisterServices();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
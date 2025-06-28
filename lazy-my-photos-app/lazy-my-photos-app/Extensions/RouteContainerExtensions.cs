using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Hosting;

namespace Lazy.MyPhotos.App.Extensions;

public static class RouteContainerExtensions
{
    public static MauiAppBuilder RegisterRoutes(this MauiAppBuilder mauiAppBuilder)
    {
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        return mauiAppBuilder;
    }

}
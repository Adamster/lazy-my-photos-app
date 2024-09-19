using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;

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
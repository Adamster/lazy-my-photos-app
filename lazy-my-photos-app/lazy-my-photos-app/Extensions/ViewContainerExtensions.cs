using Lazy.MyPhotos.App.View;
using Lazy.MyPhotos.App.View.User;

namespace Lazy.MyPhotos.App.Extensions;

public static class ViewContainerExtensions
{
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();

        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<RegisterPage>();

        return mauiAppBuilder;
    }
}
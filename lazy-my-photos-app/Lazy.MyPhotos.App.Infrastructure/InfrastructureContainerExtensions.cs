using Lazy.MyPhotos.App.Infrastructure.ApiServices;

#if WINDOWS
using Lazy.MyPhotos.App.Infrastructure.Platforms.Windows;
#endif

#if  ANDROID
using Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;
#endif

#if IOS
using Lazy.MyPhotos.App.Infrastructure.Platforms.iOS.Services;
#elif __IOS__
using Lazy.MyPhotos.App.Infrastructure.Platforms.MacCatalyst.Services;
#endif

using Lazy.MyPhotos.App.Infrastructure.Providers.Impl;
using Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces;
using Lazy.MyPhotos.App.Infrastructure.Services;
using Lazy.MyPhotos.Shared.Services.Gallery.Interfaces;

namespace Lazy.MyPhotos.App.Infrastructure
{
    public static class InfrastructureContainerExtensions
    {
        public static MauiAppBuilder RegisterInfrastructure(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.RegisterCommonServices();
            mauiAppBuilder.RegisterApiServices();


            //providers
            mauiAppBuilder.Services.AddSingleton<IDataConfigurationProvider, DataConfigurationProvider>();

            //platform services
            mauiAppBuilder.Services.AddSingleton<IGalleryService, GalleryService>();

            return mauiAppBuilder;
        }
    }
}
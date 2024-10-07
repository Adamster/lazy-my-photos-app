using Lazy.MyPhotos.App.Infrastructure.ApiServices;
using Lazy.MyPhotos.App.Infrastructure.Platforms.Android.Services;
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
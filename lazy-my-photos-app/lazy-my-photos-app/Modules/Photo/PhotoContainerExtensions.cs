using Lazy.MyPhotos.App.Modules.Photo.Handlers.Impl;
using Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces;
using Lazy.MyPhotos.App.Modules.Photo.Services.Impl;
using Lazy.MyPhotos.Shared.Services.Photo.Sync;

namespace Lazy.MyPhotos.App.Modules.Photo
{
    public static class PhotoContainerExtensions
    {
        public static MauiAppBuilder RegisterPhotoModule(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IRequestPhotoAccessPermissionHandler, RequestPhotoAccessPermissionHandler>();

            mauiAppBuilder.Services.AddSingleton<IPhotoSyncService, PhotoSyncService>();

            return mauiAppBuilder;
        }
    }
}
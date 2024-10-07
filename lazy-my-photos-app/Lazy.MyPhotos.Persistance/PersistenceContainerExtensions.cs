using Lazy.MyPhotos.Persistence.DataAccess.Common.Implementation;
using Lazy.MyPhotos.Persistence.DataAccess.Common.Interfaces;
using Lazy.MyPhotos.Persistence.DataAccess.Controllers.Impl;
using Lazy.MyPhotos.Persistence.DataAccess.Controllers.Interfaces;

namespace Lazy.MyPhotos.Persistence
{
    public static class PersistenceContainerExtensions
    {
        public static MauiAppBuilder RegisterPersistence(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<ILiteDbFactory, LiteDbFactory>();

            mauiAppBuilder.Services.AddTransient<IPhotoDataAccess, PhotoDataAccess>();

            return mauiAppBuilder;
        }
    }
}
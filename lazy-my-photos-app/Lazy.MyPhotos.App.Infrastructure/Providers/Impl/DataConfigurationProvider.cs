using Lazy.MyPhotos.App.Infrastructure.Configuration.Impl;
using Lazy.MyPhotos.App.Infrastructure.Configuration.Interfaces;
using Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Lazy.MyPhotos.App.Infrastructure.Providers.Impl
{
    internal sealed class DataConfigurationProvider : IDataConfigurationProvider
    {

        internal DataConfigurationProvider(IConfiguration configuration)
        {
            var dbName = "lazy-photos.db";
            var databasePath = $"{FileSystem.Current.AppDataDirectory}\\{dbName}";

            Configuration = new DataConfiguration(databasePath, FileSystem.Current.AppDataDirectory,
                FileSystem.Current.CacheDirectory);
        }

        public IDataConfiguration Configuration { get; }
    }
}
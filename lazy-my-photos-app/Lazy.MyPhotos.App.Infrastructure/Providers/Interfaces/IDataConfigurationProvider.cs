using Lazy.MyPhotos.App.Infrastructure.Configuration.Interfaces;

namespace Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces
{
    public interface IDataConfigurationProvider
    {
        IDataConfiguration Configuration { get; }
    }
}
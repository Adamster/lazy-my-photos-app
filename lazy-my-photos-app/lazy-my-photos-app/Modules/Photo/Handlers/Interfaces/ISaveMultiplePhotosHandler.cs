using Lazy.MyPhotos.Persistence.Entities;

namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces
{
    public interface ISaveMultiplePhotosHandler
    {
        Task<bool> ExecuteAsync(IList<LazyPhoto> photos, CancellationToken ct);
    }
}
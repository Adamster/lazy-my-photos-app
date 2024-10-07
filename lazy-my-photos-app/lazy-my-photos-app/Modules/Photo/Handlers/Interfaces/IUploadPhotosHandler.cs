using Lazy.MyPhotos.Persistence.Entities;

namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces
{
    public interface IUploadPhotosHandler
    {
        Task<IList<LazyPhoto>> ExecuteAsync(IList<LazyPhoto> photos, CancellationToken ct);
    }
}
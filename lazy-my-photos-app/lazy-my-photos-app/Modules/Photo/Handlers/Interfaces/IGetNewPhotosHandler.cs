using Lazy.MyPhotos.App.Modules.Photo.Models;

namespace Lazy.MyPhotos.App.Modules.Photo.Handlers.Interfaces
{
    public interface IGetNewPhotosHandler
    {
        Task<IList<PhotoItem>> ExecuteAsync(CancellationToken ct);
    }
}
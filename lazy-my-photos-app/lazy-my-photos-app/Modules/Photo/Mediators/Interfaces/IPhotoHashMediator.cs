using Lazy.MyPhotos.App.Modules.Photo.Models;
using Lazy.MyPhotos.Persistence.Entities;

namespace Lazy.MyPhotos.App.Modules.Photo.Mediators.Interfaces
{
    public interface IPhotoHashMediator
    {
        Task<IList<LazyPhoto>> ExecuteAsync(IList<PhotoItem> photos, CancellationToken ct);
    }
}
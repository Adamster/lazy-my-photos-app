using Lazy.MyPhotos.App.Infrastructure.ApiServices.Models.Photo;
using Lazy.MyPhotos.Persistence.Entities;

namespace Lazy.MyPhotos.Persistence.DataAccess.Controllers.Interfaces
{
    public interface IPhotoDataAccess
    {
        Task InsertAsync(LazyPhoto entity);

        Task BulkInsert(IList<LazyPhoto> photos);

        Task<IList<LazyPhoto>> GetAllAsync();

        Task<LazyPhoto?> GetByIdAsync(int id);

        Task UpdateAsync(LazyPhoto entity);
    }
}
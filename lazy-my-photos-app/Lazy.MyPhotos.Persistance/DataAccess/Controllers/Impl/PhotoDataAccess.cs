using Lazy.MyPhotos.App.Infrastructure.Constants;
using Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces;
using Lazy.MyPhotos.Persistence.DataAccess.Common.Implementation;
using Lazy.MyPhotos.Persistence.DataAccess.Common.Interfaces;
using Lazy.MyPhotos.Persistence.DataAccess.Controllers.Interfaces;
using Lazy.MyPhotos.Persistence.Entities;

namespace Lazy.MyPhotos.Persistence.DataAccess.Controllers.Impl
{
    internal class PhotoDataAccess : CoreDataAccessControllerBase, IPhotoDataAccess
    {
        public PhotoDataAccess(ILiteDbFactory liteDbFactory, IDataConfigurationProvider dataConfigurationProvider) : base(liteDbFactory, dataConfigurationProvider)
        {
        }


        public Task InsertAsync(LazyPhoto entity) => ExecuteAsync<LazyPhoto>(collection => collection.InsertAsync(entity), CollectionNames.Photos);
        public Task BulkInsert(IList<LazyPhoto> photos) => ExecuteAsync<LazyPhoto>(collection => collection.InsertBulkAsync(photos), CollectionNames.Photos);


        public Task<IList<LazyPhoto>> GetAllAsync() => ExecuteAsync<LazyPhoto>(collection => collection.Query(), CollectionNames.Photos);

        public Task<LazyPhoto?> GetByIdAsync(int id) => ExecuteSingleAsync<LazyPhoto>(photo =>  photo.Id == id, CollectionNames.Photos);

        public Task UpdateAsync(LazyPhoto entity) => ExecuteAsync<LazyPhoto>(collection => collection.UpdateAsync(entity), CollectionNames.Photos);
    }
}
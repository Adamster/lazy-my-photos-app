using Lazy.MyPhotos.App.Infrastructure.Providers.Interfaces;
using Lazy.MyPhotos.Persistence.DataAccess.Common.Interfaces;
using LiteDB.Async;
using System.Linq.Expressions;

namespace Lazy.MyPhotos.Persistence.DataAccess.Common.Implementation
{ 
    internal abstract class DataAccessControllerBase
    {
        private readonly ILiteDbFactory _liteDbFactory;
        public string ConnectionString { get; }


        protected DataAccessControllerBase(ILiteDbFactory liteDbFactory, string connectionString)
        {
            ConnectionString = connectionString;
            _liteDbFactory = liteDbFactory;
        }

        protected ILiteDatabaseAsync ConnectAsync(string databasePath) => _liteDbFactory.ConnectAsync(databasePath);

        protected async Task ExecuteAsync<TDataModel>(Func<ILiteCollectionAsync<TDataModel>, Task> func, string collectionName)
        {
            using var db = ConnectAsync(ConnectionString);
            var collection = db.GetCollection<TDataModel>(collectionName);
            await func(collection).ConfigureAwait(false);
        }
    
        protected async Task<IList<TDataModel>> ExecuteAsync<TDataModel>(Func<ILiteCollectionAsync<TDataModel>, ILiteQueryableAsync<TDataModel>> func, string collectionName)
        {
            using var db = ConnectAsync(ConnectionString);
            var collection = db.GetCollection<TDataModel>(collectionName);
            var results = await func(collection).ToListAsync().ConfigureAwait(false);
            return results;
        }
    
        protected async Task<TDataModel?> ExecuteSingleAsync<TDataModel>(Expression<Func<TDataModel, bool>> func, string collectionName)
        {
            using var db = ConnectAsync(ConnectionString);
            var collection = db.GetCollection<TDataModel>(collectionName);
            return await collection.FindOneAsync(func).ConfigureAwait(false);
        }

    }

    internal abstract class CoreDataAccessControllerBase : DataAccessControllerBase
    {
        protected CoreDataAccessControllerBase(ILiteDbFactory liteDbFactory, IDataConfigurationProvider dataConfigurationProvider)
            : base(liteDbFactory, dataConfigurationProvider.Configuration.DbFilePath)
        {
        }
    }
}
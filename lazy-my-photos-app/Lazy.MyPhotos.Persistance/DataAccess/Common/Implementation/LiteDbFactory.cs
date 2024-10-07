using Lazy.MyPhotos.Persistence.DataAccess.Common.Interfaces;
using LiteDB;
using LiteDB.Async;

namespace Lazy.MyPhotos.Persistence.DataAccess.Common.Implementation
{
    internal sealed class LiteDbFactory : ILiteDbFactory
    {
        public ILiteDatabase Connect(string databasePath) => new LiteDatabase($"Filename={databasePath}; Connection=Shared")
        {
            Mapper = { TrimWhitespace = false, EmptyStringToNull = false, SerializeNullValues = true }
        };

        public ILiteDatabaseAsync ConnectAsync(string databasePath) => new LiteDatabaseAsync(Connect(databasePath), false);
    }
}
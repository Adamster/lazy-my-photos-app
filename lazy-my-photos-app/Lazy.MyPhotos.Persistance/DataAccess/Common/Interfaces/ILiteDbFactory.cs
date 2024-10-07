using LiteDB;
using LiteDB.Async;

namespace Lazy.MyPhotos.Persistence.DataAccess.Common.Interfaces
{
    internal interface ILiteDbFactory
    {
        ILiteDatabase Connect(string databasePath);

        ILiteDatabaseAsync ConnectAsync(string databasePath);
    }
}
using Lazy.MyPhotos.Persistence.Entities;
using SQLite;

namespace Lazy.MyPhotos.Persistence;

public class LazyPhotosDatabase
{
    private SQLiteAsyncConnection? _database;

    private async Task Init()
    {
        if (_database is not null)
        {
            return;
        }

        _database = new SQLiteAsyncConnection(
        Constants.DatabasePath,
        Constants.Flags);
        await _database.CreateTableAsync<UserProfile>(); 
    }
}
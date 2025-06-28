using Microsoft.Extensions.Logging;
using SQLite;
using System.Linq.Expressions;

namespace Lazy.MyPhotos.Persistence;

public class PhotoDbContext
{
    private readonly ILogger<PhotoDbContext> _logger;

    public PhotoDbContext(ILogger<PhotoDbContext> logger)
    {
        _logger = logger;
    }

    private const string DbName = "lazy-photos.db3";

    private readonly string _dbPath = Path.Combine(FileSystem.AppDataDirectory, DbName);

    private SQLiteAsyncConnection? _connection;

    private SQLiteAsyncConnection Database =>
        _connection ??= new SQLiteAsyncConnection(_dbPath,
            SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);

    public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
    {
        var table = await GetTableAsync<TTable>();
        return await table.ToListAsync();
    }

    public async Task<IEnumerable<TTable>> GetFilteredAsync<TTable>(Expression<Func<TTable, bool>> predicate)
        where TTable : class, new()
    {

        var table = await GetTableAsync<TTable>();
        return await table.Where(predicate).ToListAsync();
    }

    public async Task<TTable> GetItemByIdAsync<TTable>(object id) where TTable : class, new()
    {
        return await Execute<TTable, TTable>(async () => await Database.GetAsync<TTable>(id));
    }

    public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        return await Execute<TTable, bool>(async () => await Database.InsertAsync(item) > 0);
    }

    public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        return await Execute<TTable, bool>(async () => await Database.UpdateAsync(item) > 0);
    }

    public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
    {
        return await Execute<TTable, bool>(async () => await Database.DeleteAsync(item) > 0);
    }

    public async Task<bool> DeleteItemByIdAsync<TTable>(object id) where TTable : class, new()
    {
        return await Execute<TTable, bool>(async () => await Database.DeleteAsync(id) > 0);
    }

    private async Task CreateTableIfNotExists<TTable>() where TTable : class, new()
    {
        _logger.LogInformation("Creating table {0} if not exists", typeof(TTable).Name);

        var createTableResult = await Database.CreateTableAsync<TTable>();
        _logger.LogInformation("database location: {0}", _dbPath);
        _logger.LogInformation("Table {1} {0}", createTableResult, typeof(TTable).Name);
    }

    private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
    {
        try
        {
            _logger.LogInformation("Executing action {0}", action.Method.Name);
            await CreateTableIfNotExists<TTable>();
            return await action();
        }
        catch (SQLiteException ex)
        {
            _logger.LogCritical(ex, "Error during database operation: {0}", ex.Message);
            throw;
        }
    }

    private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
    {
        await CreateTableIfNotExists<TTable>();
        return Database.Table<TTable>();
    }
}
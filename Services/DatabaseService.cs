using OrderManagerMaui.Models;
using SQLite;

namespace OrderManagerMaui.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection _connection;

    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "orders.db3");
        _connection = new SQLiteAsyncConnection(dbPath);
    }

    public async Task InitAsync()
    {
        await _connection.CreateTableAsync<Order>();
    }
}

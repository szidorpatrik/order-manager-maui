using OrderManagerMaui.Models;
using SQLite;

namespace OrderManagerMaui.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? _connection;

    private async Task InitAsync()
    {
        if (_connection is not null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "orders.db3");
        _connection = new SQLiteAsyncConnection(dbPath);
        await _connection.CreateTableAsync<Order>();
    }
}

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

    public async Task<List<Order>> GetOrders()
    {
        return await _connection
            .Table<Order>()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderById(int id)
    {
        return await _connection
            .Table<Order>()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> SaveOrder(Order order)
    {
        if (order.Id == 0)
        {
            return await _connection.InsertAsync(order);
        }

        return await _connection.UpdateAsync(order);
    }

    public async Task<int> DeleteOrder(Order order)
    {
        return await _connection.DeleteAsync(order);
    }
}

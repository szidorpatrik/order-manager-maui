using SQLite;

namespace OrderManagerMaui.Models;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string CustomerName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public float TotalAmount { get; set; }
    public bool IsDelivered { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

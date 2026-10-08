namespace MaisonFleurie.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}
namespace MaisonFleurie.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    public int PastryItemId { get; set; }
    public PastryItem? PastryItem { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
namespace MaisonFleurie.Models;

public class PastryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Signature, Entremet, Tart
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsNew { get; set; }
}

public class CartItem
{
    public PastryItem Item { get; set; } = new();
    public int Quantity { get; set; }
}
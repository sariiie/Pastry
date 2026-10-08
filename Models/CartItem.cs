namespace MaisonFleurie.Models;

public class CartItem
{
    public PastryItem Item { get; set; } = new();
    public int Quantity { get; set; } = 1;
}
using MaisonFleurie.Models;

namespace MaisonFleurie.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();
    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public int TotalCount => _items.Sum(i => i.Quantity);
    public decimal TotalAmount => _items.Sum(i => i.Item.Price * i.Quantity);

    public void AddItem(PastryItem item)
    {
        var existing = _items.FirstOrDefault(i => i.Item.Id == item.Id || i.Item.Name == item.Name);
        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            _items.Add(new CartItem { Item = item, Quantity = 1 });
        }
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
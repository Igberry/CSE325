using W03_BlazorApp.Models;

namespace W03_BlazorApp.Services;

public class OrderState
{
    private readonly List<OrderEntry> _items = [];
    private readonly List<PlacedOrder> _orders = [];
    private int _nextOrderId = 1;

    public IReadOnlyList<OrderEntry> Items => _items;
    public IReadOnlyList<PlacedOrder> Orders => _orders;

    public decimal Total => _items.Sum(item => item.Pizza.BasePrice * item.Quantity);

    public void AddPizza(PizzaSpecial pizza)
    {
        var existing = _items.FirstOrDefault(item => item.Pizza.Id == pizza.Id);
        if (existing is null)
        {
            _items.Add(new OrderEntry(pizza, 1));
            return;
        }

        existing.Quantity++;
    }

    public void Clear()
    {
        _items.Clear();
    }

    public PlacedOrder PlaceOrder(string customerName)
    {
        var order = new PlacedOrder
        {
            Id = _nextOrderId++,
            CustomerName = customerName,
            CreatedAt = DateTime.Now,
            Items = _items.Select(item => new OrderEntry(item.Pizza, item.Quantity)).ToList()
        };

        _orders.Add(order);
        _items.Clear();
        return order;
    }

    public PlacedOrder? GetOrder(int id) => _orders.FirstOrDefault(order => order.Id == id);
}

public class OrderEntry
{
    public OrderEntry(PizzaSpecial pizza, int quantity)
    {
        Pizza = pizza;
        Quantity = quantity;
    }

    public PizzaSpecial Pizza { get; set; }
    public int Quantity { get; set; }
}

public class PlacedOrder
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderEntry> Items { get; set; } = [];

    public decimal Total => Items.Sum(item => item.Pizza.BasePrice * item.Quantity);
}

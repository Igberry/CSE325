using W03_BlazorApp.Models;

namespace W03_BlazorApp.Services;

public class PizzaService
{
    private readonly List<PizzaSpecial> _specials =
    [
        new() { Id = 1, Name = "The Baconator", BasePrice = 12.99m, Description = "Crispy bacon, mozzarella, and rich tomato sauce.", ImageUrl = "https://images.unsplash.com/photo-1513104890138-7c749659a591?auto=format&fit=crop&w=800&q=80" },
        new() { Id = 2, Name = "Veggie Delight", BasePrice = 10.49m, Description = "Bell peppers, olives, onions, and fresh herbs.", ImageUrl = "https://images.unsplash.com/photo-1548365328-8c6e74d4d6e0?auto=format&fit=crop&w=800&q=80" },
        new() { Id = 3, Name = "Margherita", BasePrice = 9.99m, Description = "Classic tomato, mozzarella, and basil.", ImageUrl = "https://images.unsplash.com/photo-1513104890138-7c749659a591?auto=format&fit=crop&w=800&q=80" }
    ];

    public IEnumerable<PizzaSpecial> GetSpecials() => _specials;
}

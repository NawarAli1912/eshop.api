using Domain.Customers.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Customers.Entities;
public class Cart
{
    private readonly List<CartItem> _items = new();

    public CartId Id { get; private set; }

    public IImmutableList<CartItem> Items => _items.ToImmutableList();

    private Cart(CartId id)
    {
        Id = id;
    }

    public static Cart Create(CartId id)
    {
        return new(id);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Cart() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

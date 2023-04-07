using Domain.Customers.ValueObjects;
using Domain.Orders.Entities;
using Domain.Orders.Enums;
using Domain.Orders.ValueObjects;
using Domain.Products;
using Domain.SharedKernel.Primitives;
using System.Collections.Immutable;

namespace Domain.Orders;

public class Order : AggregateRoot<OrderId>
{
    private readonly HashSet<LineItem> _lineItems = new();

    public CustomerId CustomerId { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ModifiedAt { get; set; }

    public IImmutableSet<LineItem> LineItems => _lineItems.ToImmutableHashSet();

    public void AddProduct(Product product, int quantity = 1)
    {
        var lineItem = LineItem.Create(
            LineItemId.CreateNew(),
            Id,
            product.Id,
            product.Price,
            quantity);

        _lineItems.Add(lineItem);
    }

    public bool RemoveProduct(Product product)
    {
        if (!_lineItems.Any(p => p.ProductId == product.Id))
        {
            return false;
        }

        _lineItems.RemoveWhere(li => li.ProductId == product.Id);

        return true;
    }

    private Order(OrderId id, CustomerId customerId) : base(id)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public static Order Create(OrderId id, CustomerId customerId)
    {
        return new(id, customerId);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Order() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

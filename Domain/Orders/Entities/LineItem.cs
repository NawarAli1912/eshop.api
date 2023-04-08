using Domain.Orders.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Orders.Entities;

public class LineItem : Entity<LineItemId>
{
    public OrderId OrderId { get; private set; }

    public ProductId ProductId { get; private set; }

    public int Quantity { get; private set; }

    public Money Price { get; private set; }

    private LineItem(
        LineItemId id,
        OrderId orderId,
        ProductId productId,
        Money price,
        int quantity) : base(id)
    {
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public static LineItem Create(
        LineItemId id,
        OrderId orderId,
        ProductId productId,
        Money productPrice,
        int quantity = 1)
    {
        if (quantity < 1)
        {
            throw new ArgumentException(nameof(quantity));
        }

        var cost = new Money((productPrice.Amount * quantity), productPrice.Cureency);
        return new(id, orderId, productId, cost, quantity);
    }

    // 
    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private LineItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

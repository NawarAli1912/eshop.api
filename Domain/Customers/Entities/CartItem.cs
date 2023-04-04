using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Customers.Entities;
public record CartItem
{
    public CartItemId Id { get; init; }

    public ProductId ProductId { get; init; }

    public Money Price { get; init; }

    public int Quantity { get; init; }


    private CartItem(
        CartItemId id,
        ProductId productId,
        int quantity,
        Money productPrice)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
        Price = new Money(productPrice.Cureency, quantity * productPrice.Amount);
    }

    public static CartItem Create(
        CartItemId id,
        ProductId productId,
        int quantity,
        Money productPrice)
    {
        return new(id, productId, quantity, productPrice);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CartItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Customers.Entities;
public class CartItem : Entity<CartItemId>
{

    public ProductId ProductId { get; private set; }

    public Money Price { get; private set; }

    public int Quantity { get; private set; }


    private CartItem(
        CartItemId id,
        ProductId productId,
        int quantity,
        Money price) : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public static CartItem Create(
        CartItemId id,
        ProductId productId,
        int quantity,
        Money productPrice)
    {
        return new(id, productId, quantity, new Money(quantity * productPrice.Amount, productPrice.Cureency));
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private CartItem() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

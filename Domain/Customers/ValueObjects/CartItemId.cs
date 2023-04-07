namespace Domain.Customers.ValueObjects;
public class CartItemId
{
    public Guid Value { get; set; }

    private CartItemId()
    {
        Value = Guid.NewGuid();
    }

    private CartItemId(Guid id)
    {
        Value = id;
    }

    public static CartItemId CreateNew()
    {
        return new CartItemId();
    }

    public static CartItemId Create(Guid id)
    {
        return new CartItemId(id);
    }
}

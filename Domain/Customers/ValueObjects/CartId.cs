namespace Domain.Customers.ValueObjects;
public class CartId
{
    public Guid Value { get; init; }

    private CartId()
    {
        Value = Guid.NewGuid();
    }

    private CartId(Guid id)
    {
        Value = id;
    }

    public static CartId CreateNew()
    {
        return new CartId();
    }

    public static CartId Create(Guid id)
    {
        return new CartId(id);
    }
}

using Domain.SharedKernel.Primitives;

namespace Domain.Customers.ValueObjects;
public class CartItemId : ValueObject
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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using Domain.SharedKernel.Primitives;

namespace Domain.Customers.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; private set; }

    private CustomerId()
    {
        Value = Guid.NewGuid();
    }

    private CustomerId(Guid id)
    {
        Value = id;
    }

    public static CustomerId CreateNew()
    {
        return new CustomerId();
    }

    public static CustomerId Create(Guid id)
    {
        return new CustomerId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

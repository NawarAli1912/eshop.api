using Domain.SharedKernel.Primitives;

namespace Domain.Orders.ValueObjects;

public class LineItemId : ValueObject
{
    public Guid Value { get; init; }

    private LineItemId()
    {
        Value = Guid.NewGuid();
    }

    private LineItemId(Guid id)
    {
        Value = id;
    }

    public static LineItemId CreateNew()
    {
        return new LineItemId();
    }

    public static LineItemId Create(Guid id)
    {
        return new LineItemId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

using Domain.SharedKernel.Primitives;

namespace Domain.Orders.ValueObjects;

public sealed class OrderId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private OrderId()
    {
        Value = Guid.NewGuid();
    }

    private OrderId(Guid id)
    {
        Value = id;
    }

    public static OrderId CreateNew()
    {
        return new OrderId();
    }

    public static OrderId Create(Guid id)
    {
        return new OrderId(id);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
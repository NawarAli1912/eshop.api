namespace Domain.Orders.ValueObjects;

public record OrderId
{
    public Guid Value { get; init; }

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
}
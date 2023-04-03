namespace Domain.Orders.ValueObjects;

public record LineItemId
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
}

namespace Domain.Customers.ValueObjects;

public record CustomerId
{
    public Guid Value { get; init; }

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
}

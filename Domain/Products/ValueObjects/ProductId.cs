namespace Domain.Products.ValueObjects;
public record ProductId
{
    public Guid Value { get; init; }

    private ProductId()
    {
        Value = Guid.NewGuid();
    }

    private ProductId(Guid id)
    {
        Value = id;
    }

    public static ProductId CreateNew()
    {
        return new ProductId();
    }

    public static ProductId Create(Guid id)
    {
        return new ProductId(id);
    }
}

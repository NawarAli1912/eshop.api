using Domain.SharedKernel.Primitives;

namespace Domain.Products.ValueObjects;
public sealed class ProductId : ValueObject
{
    public Guid Value { get; private set; }

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

    public static ProductId Create(string id)
    {
        return new ProductId(new Guid(id));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

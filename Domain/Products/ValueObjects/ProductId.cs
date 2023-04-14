using Domain.SharedKernel.Primitives;

namespace Domain.Products.ValueObjects;
public sealed class ProductId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

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

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

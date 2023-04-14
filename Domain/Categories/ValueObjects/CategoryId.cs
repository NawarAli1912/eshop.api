using Domain.SharedKernel.Primitives;

namespace Domain.Categories.ValueObjects;

public sealed class CategoryId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CategoryId()
    {
        Value = Guid.NewGuid();
    }

    private CategoryId(Guid id)
    {
        Value = id;
    }

    public static CategoryId Create(string id)
    {
        return new CategoryId(new Guid(id));
    }

    public static CategoryId CreateNew()
    {
        return new CategoryId();
    }

    public static CategoryId Create(Guid id)
    {
        return new CategoryId(id);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

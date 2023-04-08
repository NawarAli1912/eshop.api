namespace Domain.Categories.ValueObjects;

public record CategoryId
{
    public Guid Value { get; init; }

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
}

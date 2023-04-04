namespace Domain.Products.ValueObjects;

public record ProductReviewId
{
    public Guid Value { get; init; }

    private ProductReviewId()
    {
        Value = Guid.NewGuid();
    }

    private ProductReviewId(Guid id)
    {
        Value = id;
    }

    public static ProductReviewId CreateNew()
    {
        return new ProductReviewId();
    }

    public static ProductReviewId Create(Guid id)
    {
        return new ProductReviewId(id);
    }
}

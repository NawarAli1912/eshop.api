using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;

namespace Domain.Products.Entities;
public class ProductReview
{
    public ProductReviewId Id { get; private set; }

    public string Comment { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ModifiedAt { get; private set; }

    private ProductReview(
        ProductReviewId id,
        string comment,
        CustomerId customerId)
    {
        Id = id;
        Comment = comment;
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }

    public static ProductReview Create(
        ProductReviewId id,
        string comment,
        CustomerId customerId)
    {
        return new(id, comment, customerId);
    }
}

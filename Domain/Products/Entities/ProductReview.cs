﻿using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;

namespace Domain.Products.Entities;
public class ProductReview : Entity<ProductReviewId>
{
    public string Comment { get; private set; }

    public CustomerId CustomerId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ModifiedAt { get; private set; }

    private ProductReview(
        ProductReviewId id,
        string comment,
        CustomerId customerId,
        DateTime createdAt,
        DateTime modifiedAt) : base(id)
    {
        Comment = comment;
        CustomerId = customerId;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
    }

    public static ProductReview Create(
        ProductReviewId id,
        string comment,
        CustomerId customerId)
    {
        return new(
            id,
            comment,
            customerId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProductReview() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

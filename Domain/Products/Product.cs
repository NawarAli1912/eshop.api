using Domain.Categories.ValueObjects;
using Domain.Products.Entities;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    private readonly List<CategoryId> _categoryIds = new();

    private readonly List<ProductReview> _reviews = new();

    public string Name { get; private set; } = string.Empty;

    public int Quantity { get; set; }

    public SKU SKU { get; private set; }

    public Money Price { get; private set; }

    public IImmutableList<CategoryId> CategoryIds => _categoryIds.ToImmutableList();

    public IImmutableList<ProductReview> Reviews => _reviews.ToImmutableList();

    private Product(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price) : base(id)
    {
        Name = name;
        Quantity = quantity;
        SKU = sku;
        Price = price;
    }

    public static Product Create(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price)
    {
        return new(id, name, quantity, sku, price);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}



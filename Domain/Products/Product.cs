using Domain.Categories.ValueObjects;
using Domain.Products.DomainEvents;
using Domain.Products.Entities;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Products;

public class Product : AggregateRoot<ProductId, Guid>
{
    private readonly HashSet<CategoryId> _categoryIds = new();

    private readonly List<ProductReview> _reviews = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public int Quantity { get; set; }

    public SKU SKU { get; private set; }

    public Money Price { get; private set; }

    public AverageRating AverageRating { get; private set; }

    public IReadOnlySet<CategoryId> CategoryIds => _categoryIds.ToHashSet();

    public IReadOnlyList<ProductReview> Reviews => _reviews.ToList();

    private Product(
        ProductId id,
        string name,
        string description,
        int quantity,
        SKU sku,
        Money price,
        AverageRating averageRating) : base(id)
    {
        Name = name;
        Description = description;
        Quantity = quantity;
        SKU = sku;
        Price = price;
        AverageRating = averageRating;
    }

    public static Product Create(
        ProductId id,
        string name,
        string description,
        int quantity,
        SKU sku,
        Money price)
    {
        var product = new Product(
            id,
            name,
            description,
            quantity,
            sku,
            price,
            AverageRating.Create(0, 0));

        product.RaiseDomainEvent(new ProductCreatedDomainEvent(
            id.Value.ToString(),
            name,
            description,
            price.Amount));

        return product;
    }

    public bool AddCateogry(CategoryId categoryId)
    {
        if (_categoryIds.Contains(categoryId))
        {
            return false;
        }

        _categoryIds.Add(categoryId);
        return true;
    }

    public bool RemoveCategory(CategoryId categoryId)
    {
        if (_categoryIds.Contains(categoryId))
        {
            _categoryIds.Remove(categoryId);
            return true;
        }

        return false;
    }

    public void AddRating(int rating)
    {
        AverageRating.AddRating(rating);
    }

    public void RemoveRating(int rating)
    {
        AverageRating.RemoveRating(rating);
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}



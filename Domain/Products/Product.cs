using Domain.Categories.ValueObjects;
using Domain.Products.Entities;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    private readonly HashSet<CategoryId> _categoryIds = new();

    private readonly List<ProductReview> _reviews = new();

    public string Name { get; private set; } = string.Empty;

    public int Quantity { get; set; }

    public SKU SKU { get; private set; }

    public Money Price { get; private set; }

    public AverageRating AverageRating { get; private set; }

    public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.ToList();

    public IReadOnlyList<ProductReview> Reviews => _reviews.ToList();

    private Product(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price,
        AverageRating averageRating) : base(id)
    {
        Name = name;
        Quantity = quantity;
        SKU = sku;
        Price = price;
        AverageRating = averageRating;
    }

    public static Product Create(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price)
    {
        return new(id, name, quantity, sku, price, AverageRating.Create(0, 0));
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



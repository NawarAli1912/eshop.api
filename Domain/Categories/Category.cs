using Domain.Categories.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Primitives;

namespace Domain.Categories;
public class Category : AggregateRoot<CategoryId, Guid>
{
    private readonly HashSet<ProductId> _productIds = new();

    private readonly HashSet<Category> _subcategories = new();

    public string Name { get; private set; }

    public string Description { get; private set; }

    public Category? ParentCategory { get; private set; }

    public string? ImageUrl { get; private set; }

    public bool IsFeatured { get; private set; } = false;

    public IReadOnlySet<Category> Subcategories => _subcategories.ToHashSet();

    public IReadOnlySet<ProductId> ProductIds => _productIds.ToHashSet();

    private Category(
        CategoryId id,
        string name,
        string description,
        Category? parentCategory,
        string? imageUrl = null,
        bool isFeatured = false) : base(id)
    {
        Name = name;
        Description = description;
        ParentCategory = parentCategory;
        ImageUrl = imageUrl;
        IsFeatured = isFeatured;
    }

    public static Category Create(
        CategoryId id,
        string name,
        string description,
        Category? parentCategory,
        string? imageUrl = null,
        bool isFeatured = false)
    {
        return new Category(id, name, description, parentCategory, imageUrl, isFeatured);
    }

    public bool AddSubcategory(Category subcategory)
    {
        if (_subcategories.Contains(subcategory))
        {
            return false;
        }

        subcategory.ParentCategory = this;
        _subcategories.Add(subcategory);

        return true;
    }

    public bool RemoveSubcategory(Category subcategory)
    {
        if (_subcategories.Contains(subcategory))
        {
            _subcategories.Remove(subcategory);
            return true;
        }

        return false;
    }

    #region ef
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Category() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}

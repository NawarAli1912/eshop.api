using Domain.Categories.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Categories;
public class Category
{
    private readonly HashSet<Category> _subcategories = new();

    public CategoryId Id { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public CategoryId ParentCategoryId { get; private set; }

    public string? ImageUrl { get; private set; }

    public bool IsFeatured { get; private set; } = false;

    public IImmutableSet<Category> Subcategories => _subcategories.ToImmutableHashSet();

    private Category(
        CategoryId id,
        string name,
        string description,
        CategoryId parentCategoryId,
        string? imageUrl = null,
        bool isFeatured = false)
    {
        Id = id;
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        ImageUrl = imageUrl;
        IsFeatured = isFeatured;
    }

    public static Category Create(
        CategoryId id,
        string name,
        string description,
        CategoryId parentCategoryId,
        string? imageUrl = null,
        bool isFeatured = false)
    {
        return new Category(id, name, description, parentCategoryId, imageUrl, isFeatured);
    }

    public void AddSubcategory(Category subcategory)
    {
        subcategory.ParentCategoryId = Id;
        _subcategories.Add(subcategory);
    }

    public void RemoveSubcategory(Category subcategory)
    {
        if (_subcategories.Contains(subcategory))
        {
            _subcategories.Remove(subcategory);
        }
    }
}

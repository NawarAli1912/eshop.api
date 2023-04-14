using Domain.Categories.ValueObjects;

namespace Domain.Categories.Abstraction.Services;
public interface ICategoryService
{
    Task<bool> ValidateCategories(List<CategoryId> categoryIds);
}

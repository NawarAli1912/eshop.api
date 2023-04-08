using Domain.Categories.ValueObjects;

namespace Domain.Categories.Abstraction.Repository;
public interface ICategoryRepository
{
    Task<bool> ExistsAsync(List<CategoryId> categoryIds);
}

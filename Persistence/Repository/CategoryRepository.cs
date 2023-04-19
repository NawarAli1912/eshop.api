using Domain.Categories.Abstraction.Repository;
using Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;
internal class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(List<CategoryId> categoryIds)
    {
        var missingCategories = await _context.Categories
                            .Where(c => categoryIds.Contains(CategoryId.Create(c.Id)) == false)
                            .ToListAsync();

        return !missingCategories.Any();
    }
}

using Domain.Categories.Abstraction.Repository;

namespace Persistence.Repository;
internal class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

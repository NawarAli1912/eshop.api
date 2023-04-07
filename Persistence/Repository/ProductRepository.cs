using Domain.Products.Abstraction.Repository;

namespace Persistence.Repository;
internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

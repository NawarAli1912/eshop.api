using Domain.Products;
using Domain.Products.Abstraction.Repository;

namespace Persistence.Repository;
internal class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }
}

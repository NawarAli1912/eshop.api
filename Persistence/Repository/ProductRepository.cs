using Domain.Products;
using Domain.Products.Abstraction.Repository;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;
public class ProductRepository : IProductRepository
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

    public Task<Product?> GetAsync(ProductId productId, CancellationToken c)
    {
        return _context
                .Products
                .Include(p => p.Reviews)
                .Include(p => p.CategoryIds)
                .FirstOrDefaultAsync(p => p.Id == productId.Value);
    }
}

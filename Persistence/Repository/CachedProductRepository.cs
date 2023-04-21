using Domain.Products;
using Domain.Products.Abstraction.Repository;
using Domain.Products.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repository;

public class CachedProductRepository : IProductRepository
{
    private readonly ProductRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedProductRepository(
            ProductRepository decorated,
            IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public void Add(Product product)
    {
        _decorated.Add(product);
    }

    public async Task<List<Product>> GetAllAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        string key = $"products-{pageIndex}-{pageSize}";

        var result = await _memoryCache.GetOrCreateAsync(
            key,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                return _decorated.GetAllAsync(pageIndex, pageSize, cancellationToken);
            });

        return result ?? new List<Product>();
    }

    public Task<Product?> GetAsync(
        ProductId productId,
        CancellationToken cancellationToken = default)
    {
        string key = $"product-{productId.Value}";

        return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    return _decorated.GetAsync(productId, cancellationToken);
                });
    }
}

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
        string key = $"product-{product.Id}";
        _decorated.Add(product);
        _memoryCache.Set(key, product);
    }

    public Task<Product?> GetAsync(ProductId productId, CancellationToken cancellationToken)
    {
        string key = $"product-{productId}";

        return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                    return _decorated.GetAsync(productId, cancellationToken);
                });
    }
}

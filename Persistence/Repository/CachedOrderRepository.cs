using Domain.Orders;
using Domain.Orders.Abstraction.Repository;
using Domain.Orders.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repository;
public class CachedOrderRepository : IOrderRepository
{
    private readonly OrderRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedOrderRepository(
        OrderRepository decorated,
        IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public void Add(Order order)
    {
        _decorated.Add(order);
    }

    public Task<Order?> GetAsync(OrderId orderId, CancellationToken cancellationToken = default)
    {
        string key = $"order-{orderId.Value}";

        return _memoryCache.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                    return _decorated.GetAsync(orderId, cancellationToken);
                });
    }


}

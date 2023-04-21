using Domain.Orders.ValueObjects;

namespace Domain.Orders.Abstraction.Repository;
public interface IOrderRepository
{
    void Add(Order order);

    Task<Order?> GetAsync(OrderId id, CancellationToken cancellationToken);
}

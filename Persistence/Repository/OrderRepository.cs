using Domain.Orders;
using Domain.Orders.Abstraction.Repository;
using Domain.Orders.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
    }

    public Task<Order?> GetAsync(OrderId id, CancellationToken cancellationToken)
    {
        return _context
            .Orders
            .Include(o => o.LineItems)
            .FirstOrDefaultAsync(o => o.Id == id.Value, cancellationToken);
    }
}

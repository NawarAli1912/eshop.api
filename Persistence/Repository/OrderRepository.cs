using Domain.Orders.Abstraction.Repository;

namespace Persistence.Repository;
internal class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

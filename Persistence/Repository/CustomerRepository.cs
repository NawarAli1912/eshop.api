using Domain.Customers.Abstraction.Repository;

namespace Persistence.Repository;
internal class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

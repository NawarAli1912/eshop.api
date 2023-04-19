using Domain.Categories.Abstraction.Repository;
using Domain.Customers.Abstraction.Repository;
using Domain.Orders.Abstraction.Repository;
using Domain.Products.Abstraction.Repository;
using Domain.SharedKernel.Abstraction;

namespace Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    private bool _disposed;

    public UnitOfWork(
        ApplicationDbContext context,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository)
    {
        _context = context;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }


    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository;
        }
    }

    public IOrderRepository OrderRepository
    {
        get
        {
            return _orderRepository;
        }
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            // _customerRepository ??= new CustomerRepository(_context);
            return _customerRepository;
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}

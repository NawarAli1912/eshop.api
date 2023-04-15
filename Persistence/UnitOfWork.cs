using Domain.Categories.Abstraction.Repository;
using Domain.Customers.Abstraction.Repository;
using Domain.Orders.Abstraction.Repository;
using Domain.Products.Abstraction.Repository;
using Domain.SharedKernel.Abstraction;
using Persistence.Repository;

namespace Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    private bool _disposed;
    private IProductRepository? _productRepository;
    private ICategoryRepository? _categoryRepository;
    private IOrderRepository? _orderRepository;
    private ICustomerRepository? _customerRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }


    public IProductRepository ProductRepository
    {
        get
        {
            _productRepository ??= new ProductRepository(_context);
            return _productRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            _categoryRepository ??= new CategoryRepository(_context);
            return _categoryRepository;
        }
    }

    public IOrderRepository OrderRepository
    {
        get
        {
            _orderRepository ??= new OrderRepository(_context);
            return _orderRepository;
        }
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            _customerRepository ??= new CustomerRepository(_context);
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

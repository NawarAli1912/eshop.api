using Domain.Categories.Abstraction.Repository;
using Domain.Customers.Abstraction.Repository;
using Domain.Orders.Abstraction.Repository;
using Domain.Products.Abstraction.Repository;

namespace Domain.SharedKernel.Abstraction;
public interface IUnitOfWork : IDisposable
{
    IProductRepository ProductRepository { get; }

    IOrderRepository OrderRepository { get; }

    ICustomerRepository CustomerRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    Task CommitAsync();
}

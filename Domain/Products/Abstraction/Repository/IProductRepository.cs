using Domain.Products.ValueObjects;

namespace Domain.Products.Abstraction.Repository;

public interface IProductRepository
{
    void Add(Product product);

    Task<List<Product>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

    Task<Product?> GetAsync(ProductId productId, CancellationToken cancellationToken);
}

using Domain.Products.ValueObjects;

namespace Domain.Products.Abstraction.Repository;

public interface IProductRepository
{
    void Add(Product product);

    Task<Product?> GetAsync(ProductId productId, CancellationToken cancellationToken);
}

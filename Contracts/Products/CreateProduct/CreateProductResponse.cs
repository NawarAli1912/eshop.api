using Domain.Products;

namespace Contracts.Products.CreateProduct;

public record CreateProductResponse(
        string Id,
        string Name,
        string Description,
        int Quantity,
        string SKU,
        decimal Price,
        string Currency)
{
    public static CreateProductResponse Create(Product product)
    {
        return new(
            product.Id.ToString(),
            product.Name,
            product.Description,
            product.Quantity,
            product.SKU.Value,
            product.Price.Amount,
            product.Price.Currency.ToString());
    }
}

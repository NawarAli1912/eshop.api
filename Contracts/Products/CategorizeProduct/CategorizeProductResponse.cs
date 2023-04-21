using Domain.Products;

namespace Contracts.Products.CategorizeProduct;
public record CategorizeProductResponse(
    string Id,
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    List<string>? CategoryIds)
{

    public static CategorizeProductResponse Create(Product product)
    {
        return new(
            product.Id.ToString(),
            product.Name,
            product.Description,
            product.Quantity,
            product.SKU.Value,
            product.Price.Amount,
            product.Price.Currency.ToString(),
            product.CategoryIds.Select(c => c.Value.ToString()).ToList());
    }
};

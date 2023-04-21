using Application.Products.CreateProduct;

namespace Contracts.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    List<string>? CategoryIds)
{
    public CreateProductCommand CreateCommand()
    {
        return new CreateProductCommand(
                Name,
                Description,
                Quantity,
                SKU,
                Price,
                Currency,
                CategoryIds is null ? Enumerable.Empty<string>() : CategoryIds);
    }
};

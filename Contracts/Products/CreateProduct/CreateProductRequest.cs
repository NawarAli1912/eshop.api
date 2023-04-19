namespace Contracts.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    List<string>? CategoryIds);

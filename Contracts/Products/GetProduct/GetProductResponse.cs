namespace Contracts.Products.GetProduct;

public record GetProductResponse(
    string Id,
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    double Rating,
    List<string> Reviews
    );

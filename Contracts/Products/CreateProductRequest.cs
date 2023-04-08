namespace Contracts.Products;

public record CreateProductRequest(
    string Name,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    List<string> CategoryIds);

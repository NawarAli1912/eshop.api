namespace Contracts.Products;

public record CreateProductResponse(
        string Id,
        string Name,
        int Quantity,
        string SKU,
        decimal Price,
        string Currency,
        List<string> Categories);

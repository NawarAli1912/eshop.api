using Domain.Products;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    IEnumerable<string> CategoryIds) : IRequest<Result<Product>>;

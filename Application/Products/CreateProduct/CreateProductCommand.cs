using Domain.Products;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    int Quantity,
    string SKU,
    decimal Price,
    string Currency,
    List<string> CategoryIds) : IRequest<Result<Product>>;

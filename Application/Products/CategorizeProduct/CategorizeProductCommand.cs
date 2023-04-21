using Domain.Products;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.CategorizeProduct;
public record CategorizeProductCommand(
    string ProductId,
    IEnumerable<string> CategoriesIds) : IRequest<Result<Product>>;

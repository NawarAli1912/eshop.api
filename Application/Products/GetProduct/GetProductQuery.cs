using Domain.Products;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.GetProduct;

public record GetProductQuery(string ProductId) : IRequest<Result<Product>>;
using Domain.Products;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.ListProducts;

public record ListProductsQuery(int PageIndex, int PageSize) : IRequest<Result<List<Product>>>;
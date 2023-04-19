using Domain;
using Domain.Products;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Abstraction;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.GetProduct;
internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork
                        .ProductRepository
                        .GetAsync(ProductId.Create(request.ProductId), cancellationToken);

        if (product is null)
        {
            return Errors.Product.NotExists;
        }

        return product;
    }
}

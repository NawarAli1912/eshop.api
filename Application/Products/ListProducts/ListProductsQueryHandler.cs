using Domain.Products;
using Domain.SharedKernel.Abstraction;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.ListProducts;

internal class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, Result<List<Product>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ListProductsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<Product>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await
                _unitOfWork.ProductRepository.GetAllAsync(
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken);

        return result;
    }
}

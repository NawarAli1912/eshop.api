using Domain;
using Domain.Categories.ValueObjects;
using Domain.Products;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Abstraction;
using Domain.SharedKernel.Primitives;
using MediatR;

namespace Application.Products.CategorizeProduct;
internal class CategorizeProductCommandHandler : IRequestHandler<CategorizeProductCommand, Result<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CategorizeProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Product>> Handle(CategorizeProductCommand request, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(request.ProductId);
        var categoriesIds = request
                    .CategoriesIds
                    .Select(item => CategoryId.Create(item))
                    .ToList();

        var product = await _unitOfWork.ProductRepository.GetAsync(
            productId,
            cancellationToken);

        if (product is null)
        {
            return Errors.Product.NotExists;
        }

        if (!await _unitOfWork.CategoryRepository.ExistsAsync(categoriesIds))
        {
            return Errors.Category.NotExists;
        }

        if (!product.AddCategories(categoriesIds))
        {
            return Errors.Product.FailedToAddCategory;
        }

        await _unitOfWork.CommitAsync();

        return product;
    }
}

using Domain;
using Domain.Categories.ValueObjects;
using Domain.Products;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.Abstraction;
using Domain.SharedKernel.Enums;
using Domain.SharedKernel.Primitives;
using Domain.SharedKernel.ValueObjects;
using MediatR;

namespace Application.Products.CreateProduct;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Product>> Handle(CreateProductCommand request,
            CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        List<Error> errors = new();

        var sku = SKU.Create(request.SKU);

        if (sku.IsError)
        {
            errors.AddRange(sku.Errors);

        }

        var CategoryExists = await _unitOfWork.CategoryRepository.ExistsAsync(request.CategoryIds.Select(c => CategoryId.Create(c)).ToList()).ConfigureAwait(false);

        if (!CategoryExists)
        {
            errors.Add(Errors.Category.NotExists);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        var product = Product.Create(
                        ProductId.CreateNew(),
                        request.Name,
                        "",
                        request.Quantity,
                        sku.Value,
                        Money.Create(request.Price, Currency.USD));


        _unitOfWork.ProductRepository.Add(product);

        await _unitOfWork.CommitAsync();

        return product;
    }
}

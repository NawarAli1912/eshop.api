using Application.Products.CreateProduct;
using Application.Products.GetProduct;
using Contracts.Products.CreateProduct;
using Contracts.Products.GetProduct;
using eshop.api.Common.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eshop.api.Controllers;
[Route("api/products")]
[ApiController]
public class ProductsController : ApiController
{
    public ProductsController(ISender mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var createProductResult = await Mediator.Send(
                                new CreateProductCommand(
                                            request.Name,
                                            request.Description,
                                            request.Quantity,
                                            request.SKU,
                                            request.Price,
                                            request.Currency,
                                            request.CategoryIds is null ? new List<string>() : request.CategoryIds));
        return createProductResult.Match(
            product => CreatedAtAction(
                nameof(GetProduct),
                new { product.Id },
                new CreateProductResponse(product.Id.ToString(),
                                          product.Name,
                                          product.Quantity,
                                          product.SKU.Value,
                                          product.Price.Amount,
                                          product.Price.Currency.ToString())),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(string id)
    {
        var getProductResult = await Mediator.Send(
                             new GetProductQuery(id));

        return getProductResult.Match(
            product => Ok(new GetProductResponse(
                product.Id.ToString(),
                product.Name,
                product.Description,
                product.Quantity,
                product.SKU.Value,
                product.Price.Amount,
                product.AverageRating.Value,
                product.Reviews.Select(r => r.Comment).ToList())),
            errors => Problem(errors));
    }
}

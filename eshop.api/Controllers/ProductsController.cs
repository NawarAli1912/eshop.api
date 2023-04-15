using Application.Products.CreateProduct;
using Contracts.Products;
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
                                            request.Quantity,
                                            request.SKU,
                                            request.Price,
                                            request.Currency,
                                            request.CategoryIds is null ? new List<string>() : request.CategoryIds));
        return createProductResult.Match(
            product => CreatedAtAction(
                nameof(GetProduct),
                new { product.Id },
                product),
            errors => Problem(errors));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        await Task.CompletedTask;
        return Ok();
    }
}

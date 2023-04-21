using Application.Products.CategorizeProduct;
using Application.Products.GetProduct;
using Application.Products.ListProducts;
using Contracts.Products.CategorizeProduct;
using Contracts.Products.CreateProduct;
using Contracts.Products.GetProduct;
using Contracts.Products.ListProducts;
using eshop.api.Common.Controllers;
using eshop.api.Common.Requests;
using eshop.api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eshop.api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ApiController
{
    public ProductsController(
        ISender mediator,
        IHttpContextAccessor httpContextAccessor) : base(mediator, httpContextAccessor)
    {
    }

    [HttpPost]
    [Produces(typeof(CreateProductResponse))]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var createProductResult = await Mediator.Send(request.CreateCommand());

        return createProductResult.Match(
            product => CreatedAtAction(
                nameof(GetProduct),
                new { product.Id },
                CreateProductResponse.Create(product)),
            Problem);
    }

    [HttpGet("{id}")]
    [Produces(typeof(GetProductResponse))]
    public async Task<IActionResult> GetProduct(string id)
    {
        var getProductResult = await Mediator.Send(new GetProductQuery(id));

        return getProductResult.Match(
            product => Ok(GetProductResponse.Create(product)),
            Problem);
    }

    [HttpPost("{id}/categories")]
    public async Task<IActionResult> CategorizeProduct(string id, [FromQuery] CategorizeProductRequest categorizeProductRequest)
    {
        var categorizeResult = await Mediator
                .Send(new CategorizeProductCommand(id, categorizeProductRequest.CategoriesIds));

        return categorizeResult.Match(
            product => Ok(CategorizeProductResponse.Create(product)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequest pagination)
    {
        var queryResult = await Mediator.Send(new ListProductsQuery(
            pagination.PageIndex,
            pagination.PageSize));

        return queryResult.Match(
            items => Ok(ListProductItemsResponse
                        .Create(items)
                        .ToPagedList(
                            pagination,
                            HttpContextAccessor)),
            Problem);
    }
}

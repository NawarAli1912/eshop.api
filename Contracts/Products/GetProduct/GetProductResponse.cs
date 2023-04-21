﻿using Domain.Products;

namespace Contracts.Products.GetProduct;

public record GetProductResponse(
    string Id,
    string Name,
    string Description,
    int Quantity,
    string SKU,
    decimal Price,
    double Rating,
    List<string> Reviews,
    List<string> CategoriesIds
    )
{
    public static GetProductResponse Create(Product product)
    {
        return new(
            product.Id.ToString(),
            product.Name,
            product.Description,
            product.Quantity,
            product.SKU.Value,
            product.Price.Amount,
            product.AverageRating.Value,
            product.Reviews.Select(r => r.Comment).ToList(),
            product.CategoryIds.Select(c => c.Value.ToString()).ToList());
    }
};

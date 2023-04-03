﻿using Domain.Categories.ValueObjects;
using Domain.Products.ValueObjects;
using Domain.SharedKernel.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Products;

public class Product
{
    private readonly List<CategoryId> _categoryIds = new();

    public ProductId Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public int Quantity { get; set; }

    public SKU SKU { get; private set; }

    public Money Price { get; private set; }

    public IImmutableList<CategoryId> CategoryIds => _categoryIds.ToImmutableList();

    private Product(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        SKU = sku;
        Price = price;
    }

    public static Product Create(
        ProductId id,
        string name,
        int quantity,
        SKU sku,
        Money price)
    {
        return new(id, name, quantity, sku, price);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}


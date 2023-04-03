﻿using Domain.Products;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", Schemas.Product);
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                value => ProductId.Create(value)
                );

        builder
            .OwnsOne(p => p.Price, pc =>
            {
                pc.Property(m => m.Amount)
                    .HasColumnType("decimal(10, 2)");
            });

        builder
            .OwnsOne(p => p.SKU);

        builder.OwnsMany(p => p.CategoryIds, cBuilder =>
        {
            cBuilder
                .ToTable("ProductCategoryIds", Schemas.Product);

            cBuilder
                .WithOwner()
                .HasForeignKey("ProductId");

            cBuilder
                .Property(c => c.Value)
                .HasColumnName("CategoryId")
                .ValueGeneratedNever();

            cBuilder.HasKey("Id", "ProductId");
        });
    }
}
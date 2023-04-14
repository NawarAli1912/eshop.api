using Domain.Customers.ValueObjects;
using Domain.Products;
using Domain.Products.Entities;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
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
            .OwnsOne(p => p.Price, pBuilder =>
            {
                pBuilder
                    .Property(m => m.Amount)
                    .HasColumnType("decimal(10, 2)");
            });

        builder
            .Property(p => p.Name)
            .HasMaxLength(256);

        builder
            .Property(p => p.Description)
            .HasMaxLength(512);

        builder
            .HasMany(p => p.Reviews)
            .WithOne();

        builder
            .OwnsOne(p => p.SKU, skuBuilder =>
            {
                skuBuilder
                    .Property(sku => sku.Value)
                    .HasMaxLength(15);
            });

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


        builder.OwnsOne(p => p.AverageRating);

        builder.Metadata.FindNavigation(nameof(Product.CategoryIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Product.Reviews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

internal sealed class ProductReviewConfig : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        builder.ToTable("ProductReviews", Schemas.Product);

        builder
            .HasKey(pr => pr.Id);

        builder
            .Property(pr => pr.Id)
            .HasConversion(
                productReviewId => productReviewId.Value,
                value => ProductReviewId.Create(value));

        builder
            .Property(pr => pr.CustomerId)
            .HasConversion(
                customerId => customerId.Value,
                value => CustomerId.Create(value));

        builder
            .Property(pr => pr.Comment)
            .HasMaxLength(512)
            .IsRequired(true);
    }
}

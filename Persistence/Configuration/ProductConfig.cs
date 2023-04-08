using Domain.Customers.ValueObjects;
using Domain.Products;
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

        builder.OwnsMany(p => p.Reviews, rBuilder =>
        {
            rBuilder
                .ToTable("ProductReviews", Schemas.Product);

            rBuilder
                .Property(review => review.Id)
                .HasConversion(
                    reviewId => reviewId.Value,
                    value => ProductReviewId.Create(value))
                .HasColumnName("ReviewId");

            rBuilder
                .WithOwner()
                .HasForeignKey("ProductId");

            rBuilder
                .Property(review => review.CustomerId)
                .HasConversion(
                    customerId => customerId.Value,
                    value => CustomerId.Create(value));

            rBuilder.HasKey("Id", "ProductId");

            rBuilder
            .Property(p => p.Comment)
            .HasMaxLength(256);
        });

        builder.OwnsOne(p => p.AverageRating);

        builder.Metadata.FindNavigation(nameof(Product.CategoryIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata.FindNavigation(nameof(Product.Reviews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

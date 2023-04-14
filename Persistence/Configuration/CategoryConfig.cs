using Domain.Categories;
using Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories", Schemas.Category);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                categoryId => categoryId.Value,
                value => CategoryId.Create(value));

        builder.HasMany(c => c.Subcategories)
            .WithOne(c => c.ParentCategory)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.OwnsMany(c => c.ProductIds, pBuilder =>
        {
            pBuilder
                .ToTable("CategoryProductIds", Schemas.Product);

            pBuilder
                .WithOwner()
                .HasForeignKey("CategoryId");

            pBuilder
                .Property(p => p.Value)
                .HasColumnName("ProductId")
                .ValueGeneratedNever();

            pBuilder
                .HasKey("Id", "CategoryId");
        });

        builder
            .Property(c => c.Name)
            .HasMaxLength(64);

        builder
            .Property(c => c.Description)
            .HasMaxLength(512);

        builder
            .Property(c => c.ImageUrl)
            .HasMaxLength(256);

        builder.Metadata.FindNavigation(nameof(Category.ProductIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

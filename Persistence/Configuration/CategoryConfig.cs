using Domain.Categories;
using Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal class CategoryConfig : IEntityTypeConfiguration<Category>
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
            .WithOne()
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(c => c.ParentCategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                value => CategoryId.Create(value));
    }
}

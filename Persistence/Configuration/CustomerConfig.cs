using Domain.Customers;
using Domain.Customers.Entities;
using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", Schemas.Customer);

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.FirstName)
            .HasMaxLength(128);

        builder
            .Property(c => c.LastName)
            .HasMaxLength(128);

        builder
            .Property(c => c.Email)
            .HasMaxLength(256);

        builder
            .HasIndex(c => c.Email)
            .IsUnique();

        builder
            .HasOne(c => c.Cart)
            .WithOne()
            .HasForeignKey<Customer>("CartId");
    }
}

internal sealed class CartConfig : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder
            .ToTable("Cart", Schemas.Customer);

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(
                cartId => cartId.Value,
                value => CartId.Create(value));

        builder
            .HasMany(c => c.Items)
            .WithOne();
    }
}

internal sealed class CartItemConfig : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder
            .ToTable("CartItems", Schemas.Customer);

        builder
            .HasKey(ci => ci.Id);

        builder
            .Property(ci => ci.Id)
            .HasConversion(
                cartItemId => cartItemId.Value,
                value => CartItemId.Create(value));

        builder
            .Property(ci => ci.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => ProductId.Create(value));

        builder.OwnsOne(ci => ci.Price, pBuilder =>
        {
            pBuilder
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");
        });
    }
}

using Domain.Customers;
using Domain.Customers.ValueObjects;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", Schemas.Customer);

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(
                customerId => customerId.Value,
                value => CustomerId.Create(value));

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
            .OwnsOne(c => c.Cart, cBuilder =>
            {
                cBuilder
                    .ToTable("CustomersCart", Schemas.Customer);

                cBuilder
                    .Property(cart => cart.Id)
                    .HasConversion(
                        cartId => cartId.Value,
                        value => CartId.Create(value))
                    .HasColumnName("CartId");

                cBuilder
                    .WithOwner()
                    .HasForeignKey("CustomerId");

                cBuilder.HasKey("Id", "CustomerId");

                cBuilder
                    .HasIndex("CustomerId");

                cBuilder.OwnsMany(cart => cart.Items, ciBuilder =>
                {
                    ciBuilder.Property(ci => ci.ProductId)
                        .HasConversion(
                            productId => productId.Value,
                            value => ProductId.Create(value));

                    ciBuilder.Property(ci => ci.Id)
                        .HasConversion(
                            cartItemId => cartItemId.Value,
                            value => CartItemId.Create(value))
                        .ValueGeneratedNever()
                        .HasColumnName("CartItemId");

                    ciBuilder
                        .WithOwner()
                        .HasForeignKey("CartId", "CustomerId");

                    ciBuilder
                        .HasKey("Id", "CartId", "CustomerId");

                    ciBuilder
                        .HasIndex("CartId");

                    ciBuilder
                        .OwnsOne(ci => ci.Price, pBuilder =>
                        {
                            pBuilder
                                .Property(p => p.Amount)
                                .HasColumnType("decimal(10, 2)");
                        });
                });
            });
    }
}

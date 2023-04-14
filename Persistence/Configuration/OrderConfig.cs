using Domain.Customers.ValueObjects;
using Domain.Orders;
using Domain.Orders.Entities;
using Domain.Orders.ValueObjects;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal sealed class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", Schemas.Order);

        builder
            .HasKey(o => o.Id);

        builder
            .Property(o => o.Id)
            .HasConversion(
                orderId => orderId.Value,
                value => OrderId.Create(value));

        builder
            .Property(o => o.CustomerId)
            .HasConversion(
                customerId => customerId.Value,
                value => CustomerId.Create(value));

        builder
            .HasMany(o => o.LineItems)
            .WithOne(li => li.Order);

    }
}

internal sealed class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
{
    public void Configure(EntityTypeBuilder<LineItem> builder)
    {
        builder.ToTable("LineItems", Schemas.Order);

        builder
            .HasKey(li => li.Id);

        builder
            .Property(li => li.Id)
            .HasConversion(
                lineItemId => lineItemId.Value,
                value => LineItemId.Create(value));

        builder
            .Property(li => li.ProductId)
            .HasConversion(
                productId => productId.Value,
                value => ProductId.Create(value));

        builder.OwnsOne(li => li.Price, pBuilder =>
        {
            pBuilder.Property(p => p.Amount)
                .HasColumnType("decimal(10, 2)");
        });
    }
}

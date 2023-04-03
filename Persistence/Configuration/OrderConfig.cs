using Domain.Customers.ValueObjects;
using Domain.Orders;
using Domain.Orders.ValueObjects;
using Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

internal class OrderConfig : IEntityTypeConfiguration<Order>
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
            .OwnsMany(o => o.LineItems, liBuilder =>
            {
                liBuilder.ToTable("LineItems", Schemas.Order);

                liBuilder
                    .WithOwner()
                    .HasForeignKey("OrderId");

                liBuilder
                    .Property(li => li.Id)
                    .HasColumnName("LineItemId")
                    .HasConversion(
                        lineItemId => lineItemId.Value,
                        value => LineItemId.Create(value));

                liBuilder
                    .HasKey("Id", "OrderId");

                liBuilder
                    .OwnsOne(p => p.Price, pc =>
                    {
                        pc.Property(m => m.Amount)
                            .HasColumnType("decimal(10, 2)");
                    });

                liBuilder.Property(li => li.ProductId)
                    .HasConversion(
                        productId => productId.Value,
                        value => ProductId.Create(value));
            });
    }
}

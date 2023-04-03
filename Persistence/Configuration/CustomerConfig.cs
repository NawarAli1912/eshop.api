using Domain.Customers;
using Domain.Customers.ValueObjects;
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
    }
}

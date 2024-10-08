using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TektonChallenge.Core.Products.Models;
using TektonChallenge.Infrastructure.Persistence.Converters;

namespace TektonChallenge.Infrastructure.Persistence.Configurations;

internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductId)
            .HasMaxLength(26)
            .HasConversion<UlidToStringConverter>()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(19, 4)
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .IsRequired();
    }
}
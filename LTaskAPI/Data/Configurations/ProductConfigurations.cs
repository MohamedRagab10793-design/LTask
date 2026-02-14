using LTaskAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LTaskAPI.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
        builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
    }
}

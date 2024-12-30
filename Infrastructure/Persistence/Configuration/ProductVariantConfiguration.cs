using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable(nameof(ProductVariant));
        builder.HasKey(v => v.VariantId);

        builder.Property(v => v.Color).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Storage).IsRequired();
        builder.Property(v => v.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(v => v.Stock).IsRequired();

        builder.Property(v => v.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(v => v.CreatedById).IsRequired();
        builder.Property(v => v.CreatedDate).IsRequired();
        builder.Property(v => v.UpdatedById).IsRequired(false);
        builder.Property(v => v.UpdatedDate).IsRequired(false);

        builder.HasOne(v => v.Product)
            .WithMany(p => p.ProductVariants)
            .HasForeignKey(v => v.ProductId);
    }
}

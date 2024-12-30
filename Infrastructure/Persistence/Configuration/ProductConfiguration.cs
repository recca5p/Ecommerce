using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        builder.HasKey(x => x.ProductId);

        builder.Property(x => x.ProductName).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Description).IsRequired(false);
        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(_ => _.CreatedById).IsRequired();
        builder.Property(_ => _.CreatedDate).IsRequired().HasDefaultValueSql("GETDATE()");
        builder.Property(_ => _.UpdatedById).IsRequired(false);
        builder.Property(_ => _.UpdatedDate).IsRequired(false);

        // Product -> Category
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // Product -> ProductVariant
        builder.HasMany(p => p.ProductVariants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId);

        // Product -> Image
        builder.HasMany(p => p.Images)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);
    }
}


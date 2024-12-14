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
        builder.Property(_ => _.CreatedDate).IsRequired().HasDefaultValueSql();
        builder.Property(_ => _.UpdateById).IsRequired(false);
        builder.Property(_ => _.UpdatedDate).IsRequired(false);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}
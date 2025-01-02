using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));
        builder.HasKey(x => x.CategoryId);

        builder.Property(x => x.CategoryName)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(_ => _.CreatedById).IsRequired();
        builder.Property(_ => _.CreatedDate).IsRequired();
        builder.Property(_ => _.UpdatedById).IsRequired(false);
        builder.Property(_ => _.UpdatedDate).IsRequired(false);

        // Category -> Product
        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        // Constraints
        builder.HasIndex(x => x.CategoryName)
            .IsUnique();
    }
}


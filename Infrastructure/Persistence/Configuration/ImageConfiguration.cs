using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable(nameof(Image));
        builder.HasKey(i => i.ImageId);

        builder.Property(i => i.ImageUrl).IsRequired();
        builder.Property(i => i.IsPrimary).IsRequired();

        builder.Property(i => i.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(i => i.CreatedById).IsRequired();
        builder.Property(i => i.CreatedDate).IsRequired();
        builder.Property(i => i.UpdatedById).IsRequired(false);
        builder.Property(i => i.UpdatedDate).IsRequired(false);

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Images)
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.ProductVariant)
            .WithMany()
            .HasForeignKey(i => i.VariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

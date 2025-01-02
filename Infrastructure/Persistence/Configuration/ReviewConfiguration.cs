using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(nameof(Review));
        builder.HasKey(r => r.ReviewId);

        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.Comment).IsRequired(false).HasMaxLength(1000);

        builder.Property(r => r.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(r => r.CreatedById).IsRequired();
        builder.Property(r => r.CreatedDate).IsRequired();
        builder.Property(r => r.UpdatedById).IsRequired(false);
        builder.Property(r => r.UpdatedDate).IsRequired(false);

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

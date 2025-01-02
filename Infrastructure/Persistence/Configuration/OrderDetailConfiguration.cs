using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(nameof(OrderDetail));
        builder.HasKey(od => od.OrderDetailId);

        builder.Property(od => od.Quantity).IsRequired();
        builder.Property(od => od.Price).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(od => od.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(od => od.CreatedById).IsRequired();
        builder.Property(od => od.CreatedDate).IsRequired();
        builder.Property(od => od.UpdatedById).IsRequired(false);
        builder.Property(od => od.UpdatedDate).IsRequired(false);

        builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        builder.HasOne(od => od.ProductVariant)
            .WithMany()
            .HasForeignKey(od => od.VariantId);
    }
}

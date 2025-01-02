using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));
        builder.HasKey(o => o.OrderId);

        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(o => o.PaymentStatus).IsRequired();
        builder.Property(o => o.ShippingStatus).IsRequired();

        builder.Property(o => o.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(o => o.CreatedById).IsRequired();
        builder.Property(o => o.CreatedDate).IsRequired();
        builder.Property(o => o.UpdatedById).IsRequired(false);
        builder.Property(o => o.UpdatedDate).IsRequired(false);

        builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

        builder.HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);
    }
}

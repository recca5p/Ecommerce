using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(nameof(Payment));
        builder.HasKey(p => p.PaymentId);

        builder.Property(p => p.PaymentMethod).IsRequired();
        builder.Property(p => p.PaymentDate).IsRequired();
        builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(p => p.CreatedById).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired();
        builder.Property(p => p.UpdatedById).IsRequired(false);
        builder.Property(p => p.UpdatedDate).IsRequired(false);

        builder.HasOne(p => p.Order)
            .WithOne(o => o.Payment)
            .HasForeignKey<Payment>(p => p.OrderId);
    }
}

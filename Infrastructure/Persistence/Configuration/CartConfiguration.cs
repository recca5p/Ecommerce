using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable(nameof(Cart));
        builder.HasKey(c => c.CartId);

        builder.Property(c => c.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(c => c.CreatedById).IsRequired();
        builder.Property(c => c.CreatedDate).IsRequired();
        builder.Property(c => c.UpdatedById).IsRequired(false);
        builder.Property(c => c.UpdatedDate).IsRequired(false);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);
    }
}

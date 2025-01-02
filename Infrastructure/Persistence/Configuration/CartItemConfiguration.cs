using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(nameof(CartItem));
        builder.HasKey(ci => ci.CartItemId);

        builder.Property(ci => ci.Quantity).IsRequired();

        builder.Property(ci => ci.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(ci => ci.CreatedById).IsRequired();
        builder.Property(ci => ci.CreatedDate).IsRequired();
        builder.Property(ci => ci.UpdatedById).IsRequired(false);
        builder.Property(ci => ci.UpdatedDate).IsRequired(false);

        builder.HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);

        builder.HasOne(ci => ci.ProductVariant)
            .WithMany()
            .HasForeignKey(ci => ci.VariantId);
    }
}

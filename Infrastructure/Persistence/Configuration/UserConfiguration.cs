using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.Name).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Password).IsRequired();

        builder.Property(u => u.IsDeleted).IsRequired().HasDefaultValue(false);
        builder.Property(u => u.CreatedById).IsRequired();
        builder.Property(u => u.CreatedDate).IsRequired();
        builder.Property(u => u.UpdatedById).IsRequired(false);
        builder.Property(u => u.UpdatedDate).IsRequired(false);
    }
}

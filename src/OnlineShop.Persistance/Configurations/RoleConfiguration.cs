using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Persistance.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="Role"/> entity.
/// </summary>
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(role => role.Id);

        builder.OwnsOne(role => role.Name, nameBuilder =>
        {
            nameBuilder.WithOwner();

            nameBuilder.Property(name => name.Value)
                .HasColumnName(nameof(Role.Name))
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });

        builder.Property(role => role.Description);

        builder.Property(role => role.CreatedOnUtc).IsRequired();

        builder.Property(role => role.ModifiedOnUtc);

        builder.Property(role => role.DeletedOnUtc);

        builder.Property(role => role.Deleted).HasDefaultValue(false);

        builder.HasQueryFilter(role => !role.Deleted);
    }
}

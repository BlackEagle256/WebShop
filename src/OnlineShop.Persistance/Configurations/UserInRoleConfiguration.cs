using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistance.Configurations;

/// <summary>
/// Represents the configuration for the <see cref="UserInRole"/> entity.
/// </summary>
internal sealed class UserInRoleConfiguration : IEntityTypeConfiguration<UserInRole>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserInRole> builder)
    {
        builder.HasKey(userInRole =>new
        {
            userInRole.UserId,
            userInRole.RoleId
        });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(userInRole=> userInRole.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(userInRole=> userInRole.RoleId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(userInRole => userInRole.CreatedOnUtc).IsRequired();

        builder.Property(userInRole => userInRole.ModifiedOnUtc);

        builder.Ignore(userInRole => userInRole.Id);
    }
}
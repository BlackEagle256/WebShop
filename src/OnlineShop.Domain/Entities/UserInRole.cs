using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Abstractions.Primitives;
using OnlineShop.Domain.Utility;

namespace OnlineShop.Domain.Entities;

/// <summary>
/// Represents the userInRole entity.
/// </summary>
public sealed class UserInRole : Entity, IAuditableEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInRole"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
#pragma warning disable
    private UserInRole()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInRole"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    public UserInRole(User user, Role role)
    {
        Ensure.NotNull(user, "The user is required.", nameof(user));
        Ensure.NotNull(role, "The role is required.", nameof(role));

        UserId = user.Id;
        RoleId = role.Id;
    }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; }
}
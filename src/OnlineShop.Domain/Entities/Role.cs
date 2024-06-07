using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Abstractions.Primitives;
using OnlineShop.Domain.Utility;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Domain.Entities;
/// <summary>
/// Represents the role entity.
/// </summary>
public sealed class Role : Entity, IAuditableEntity, ISoftDeletableEntity
{
    /// <summary>
    /// Gets the role name.
    /// </summary>
    public Name Name { get; private set; }
    /// <summary>
    /// Gets the role description.
    /// </summary>
    public string? Description { get; private set; }
    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; }

    /// <inheritdoc />
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc />
    public bool Deleted { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
#pragma warning disable
    private Role()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class.
    /// </summary>
    /// <param name="name">The role name</param>
    /// <param name="description">The role description</param>
    private Role(Name name, string? description = null)
        : base(Guid.NewGuid())
    {
        Ensure.NotEmpty(name, "The name is required.", nameof(Name));

        Name = name;
        Description = description;
    }
    /// <summary>
    /// Creates a new role with the specified name and description.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="description">The description.</param>
    /// <returns>The newly created role instance.</returns>
    public static Role Create(Name name, string? description = null)
    {
        return new Role(name, description);
    }
    /// <summary>
    /// Change name and description of role
    /// </summary>
    /// <param name="name">The name</param>
    /// <param name="description">The description.</param>
    public void Change(Name name, string? description = null)
    {
        Ensure.NotEmpty(name, "The name is required.", nameof(Name));

        Name = name;
        Description = description;
    }
}

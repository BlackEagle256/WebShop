using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Domain.Repositories;

/// <summary>
/// Represents the role repository interface.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Create role.
    /// </summary>
    /// <param name="role">The role.</param>
    void Insert(Role role);
    /// <summary>
    /// Update existing role.
    /// </summary>
    /// <param name="role">The role.</param>
    void Update(Role role);
    /// <summary>
    /// Checks if role exist in database.
    /// </summary>
    /// <param name="name">The role name.</param>
    /// <returns>True if the specified role exist, otherwise false.</returns>
    Task<bool> IsRoleExist(Name name);

    /// <summary>
    /// Gets the role with the specified identifier.
    /// </summary>
    /// <param name="id">The role identifier.</param>
    /// <returns>The maybe instance that may contain the role.</returns>

    Task<Maybe<Role>> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets the role with the specified name.
    /// </summary>
    /// <param name="name">The role name.</param>
    /// <returns>The maybe instance that may contain the role.</returns>

    Task<Maybe<Role>> GetByNameAsync(Name name);


}

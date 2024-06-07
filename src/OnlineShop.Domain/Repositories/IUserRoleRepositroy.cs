using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Repositories;
/// <summary>
/// Represents the userRole repository interface.
/// </summary>
public interface IUserRoleRepositroy
{
    /// <summary>
    /// Adds role to the user.
    /// </summary>
    /// <param name="userInRole">The userInRole.</param>
    void AddRoleToUser(UserInRole userInRole);

    /// <summary>
    /// Checks if user has the specific role
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    /// <returns>True if the specified users has role, otherwise false.</returns>
    Task<bool> IsUserInRole(User user, Role role);

    /// <summary>
    /// Gets User's Roles
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The specified number of userInRole, if any exist.</returns>
    Task<IReadOnlyCollection<UserInRole>> GetUserRoles(User user);
    /// <summary>
    /// Remove role from user.
    /// </summary>
    /// <param name="userInRole">The userInRole.</param>
    void RemoveRoleFromUser(UserInRole userInRole);
}

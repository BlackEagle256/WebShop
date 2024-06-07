using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Repositories;
using OnlineShop.Persistance.Specifications;

namespace OnlineShop.Persistance.Repositories;

/// <summary>
/// Represents the userInRole repository.
/// </summary>
internal sealed class UserRoleRepositroy : GenericRepository<UserInRole>, IUserRoleRepositroy
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoleRepositroy"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public UserRoleRepositroy(IDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc />
    public void AddRoleToUser(UserInRole userInRole) => Insert(userInRole);

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<UserInRole>> GetUserRoles(User user) =>
        await DbContext.Set<UserInRole>()
            .Where(new UserWithRolesSpecification(user))
            .ToArrayAsync();


    /// <inheritdoc />
    public async Task<bool> IsUserInRole(User user, Role role) => await AnyAsync(new UserWithRoleSpecification(user, role));

    /// <inheritdoc />
    public void RemoveRoleFromUser(UserInRole userInRole) => Remove(userInRole);
}

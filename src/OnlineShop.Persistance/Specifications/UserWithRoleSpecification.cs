using OnlineShop.Domain.Entities;
using System.Linq.Expressions;

namespace OnlineShop.Persistance.Specifications;

/// <summary>
/// Represents the specification for determining the user has role
/// </summary>
internal sealed class UserWithRoleSpecification : Specification<UserInRole>
{
    private readonly User _user;
    private readonly Role _role;
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithRoleSpecification"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    internal UserWithRoleSpecification(User user, Role role) => (_user, _role) = (user, role);

    /// <inheritdoc />
    internal override Expression<Func<UserInRole, bool>> ToExpression()
        => userInRole => userInRole.UserId == _user.Id && userInRole.RoleId == _role.Id;
}
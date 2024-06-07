using OnlineShop.Domain.Entities;
using System.Linq.Expressions;

namespace OnlineShop.Persistance.Specifications;

/// <summary>
/// Represents the specification for Getting the user roles.
/// </summary>
internal sealed class UserWithRolesSpecification : Specification<UserInRole>
{
    private readonly User _user;
    /// <summary>
    /// Initializes a new instance of the <see cref="UserWithRolesSpecification"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    internal UserWithRolesSpecification(User user) => _user = user;

    /// <inheritdoc />
    internal override Expression<Func<UserInRole, bool>> ToExpression()
        => userInRole => userInRole.UserId == _user.Id;
}

using OnlineShop.Domain.Entities;
using OnlineShop.Domain.ValueObjects;
using System.Linq.Expressions;

namespace OnlineShop.Persistance.Specifications;

/// <summary>
/// Represents the specification for Getting the role.
/// </summary>
internal sealed class RoleNameSpecification : Specification<Role>
{
    private readonly Name _name;
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleNameSpecification"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    internal RoleNameSpecification(Name name) => _name = name;

    /// <inheritdoc />
    internal override Expression<Func<Role, bool>> ToExpression()
        => role => role.Name.Value == _name;
}

using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Contracts.Roles;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Roles.Queries.GetRoleByName;

public sealed class GetRoleByNameQuery : IQuery<Maybe<RoleResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetRoleByNameQuery"/> class.
    /// </summary>
    /// <param name="name">The role name</param>
    public GetRoleByNameQuery(string name)
    {
        Name = name;
    }
    /// <summary>
    /// Gets the role name.
    /// </summary>
    public string Name { get; }
}
internal sealed class GetRoleByNameQueryHandler : IQueryHandler<GetRoleByNameQuery, Maybe<RoleResponse>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRoleByNameQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Maybe<RoleResponse>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        Result<Name> resultName = Name.Create(request.Name);

        if (resultName.IsFailure)
        {
            return Maybe<RoleResponse>.None;
        }

        Maybe<Role> maybeRole = await _roleRepository.GetByNameAsync(resultName.Value);

        if (maybeRole.HasNoValue)
        {
            return Maybe<RoleResponse>.None;
        }

        Role role = maybeRole.Value;

        return new RoleResponse
        {
            Id = role.Id,
            Description = role.Description,
            Name = role.Name
        };
    }
}
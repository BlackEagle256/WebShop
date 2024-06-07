using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Contracts.Users;
using OnlineShop.Domain.Abstractions.Maybe;

namespace OnlineShop.Application.Users.Queries.GetUserById;
/// <summary>
/// Represents the query for getting a user by identifier.
/// </summary>
public sealed class GetUserByIdQuery : IQuery<Maybe<UserResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public GetUserByIdQuery(Guid userId) => UserId = userId;

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public Guid UserId { get; }
}

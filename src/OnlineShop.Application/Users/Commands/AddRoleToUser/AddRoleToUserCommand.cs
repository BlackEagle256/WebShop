using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Result;

namespace OnlineShop.Application.Users.Commands.AddRoleToUser;
/// <summary>
/// Represents the add role to user command.
/// </summary>
public sealed class AddRoleToUserCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddRoleToUserCommand"/> class.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="role">The role name.</param>
    public AddRoleToUserCommand(Guid userId, string role)
    {
        UserId = userId;
        Role = role;
    }
    /// <summary>
    /// Gets the user id.
    /// </summary>
    public Guid UserId { get; }
    /// <summary>
    /// Gets the role name.
    /// </summary>
    public string Role { get; }
}

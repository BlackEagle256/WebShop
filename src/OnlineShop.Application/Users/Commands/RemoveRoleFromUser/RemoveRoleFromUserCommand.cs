using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Application.Users.Commands.AddRoleToUser;
using OnlineShop.Domain.Abstractions.Result;

namespace OnlineShop.Application.Users.Commands.RemoveRoleFromUser;

/// <summary>
/// Represents the add role to user command.
/// </summary>
public sealed class RemoveRoleFromUserCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RemoveRoleFromUserCommand"/> class.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="role">The role name.</param>
    public RemoveRoleFromUserCommand(Guid userId, string role)
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

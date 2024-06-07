using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Result;

namespace OnlineShop.Application.Roles.Commands.CreateRole;
/// <summary>
/// Represents the create role command.
/// </summary>
public sealed class CreateRoleCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRoleCommand"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="description">The description</param>
    public CreateRoleCommand(string name, string? description)
    {
        Name = name;
        Description = description;
    }
    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get;  }
    /// <summary>
    /// Gets the description.
    /// </summary>
    public string? Description { get; }
}

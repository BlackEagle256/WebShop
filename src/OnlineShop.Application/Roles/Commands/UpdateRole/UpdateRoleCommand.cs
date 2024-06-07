using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Result;

namespace OnlineShop.Application.Roles.Commands.UpdateRole;
/// <summary>
/// Represents the update role command.
/// </summary>
public sealed class UpdateRoleCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRoleCommand"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="name">The name.</param>
    /// <param name="description">The description</param>
    public UpdateRoleCommand(Guid id, string name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    /// <summary>
    /// Gets the id
    /// </summary>
    public Guid Id { get; }
    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Gets the description.
    /// </summary>
    public string? Description { get; }
}

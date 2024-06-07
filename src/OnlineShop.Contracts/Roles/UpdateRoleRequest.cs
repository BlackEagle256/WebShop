namespace OnlineShop.Contracts.Roles;

/// <summary>
/// Represents the update role request.
/// </summary>
public sealed class UpdateRoleRequest
{
    /// <summary>
    /// Gets or sets the role identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the role description.
    /// </summary>
    public string? Description { get; set; }
}

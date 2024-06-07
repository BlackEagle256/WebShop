namespace OnlineShop.Contracts.Roles;
/// <summary>
/// Represents the create role request.
/// </summary>
public sealed class CreateRoleRequest
{
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the role description.
    /// </summary>
    public string? Description { get; set; }
}

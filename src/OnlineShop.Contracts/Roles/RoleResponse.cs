namespace OnlineShop.Contracts.Roles;
/// <summary>
/// Represents the role response.
/// </summary>
public sealed class RoleResponse
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

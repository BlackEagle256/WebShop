namespace OnlineShop.Contracts.Users;

/// <summary>
/// Represents the add role to user request.
/// </summary>
public sealed class AddUserToRoleRequest
{
    /// <summary>
    /// Gets or sets the user identifier.
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Role { get; set; }
}

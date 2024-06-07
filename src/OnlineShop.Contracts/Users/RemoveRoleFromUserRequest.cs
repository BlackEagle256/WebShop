namespace OnlineShop.Contracts.Users;

/// <summary>
/// Represents the remove role from user request.
/// </summary>
public sealed class RemoveRoleFromUserRequest
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
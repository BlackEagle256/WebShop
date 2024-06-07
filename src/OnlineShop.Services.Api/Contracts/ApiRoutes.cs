namespace OnlineShop.Services.Api.Contracts;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the authentication routes.
    /// </summary>
    public static class Authentication
    {
        public const string Login = "authentication/login";

        public const string Register = "authentication/register";
    }

    /// <summary>
    /// Contains the users routes.
    /// </summary>
    public static class Users
    {
        public const string GetById = "users/{userId:guid}";

        public const string Update = "users/{userId:guid}";

        public const string ChangePassword = "users/{userId:guid}/change-passwrod";

        public const string AddRole = "users/{userId:guid}/add-role";

        public const string RemoveRole = "users/{userId:guid}/remove-role";

    }
    /// <summary>
    /// Contains the roles routes.
    /// </summary>
    public static class Roles
    {
        public const string GetByName = "roles/{roleName}";

        public const string create = "roles/create";

        public const string update = "roles/update";
    }


}
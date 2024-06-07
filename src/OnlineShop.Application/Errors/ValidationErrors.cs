using OnlineShop.Domain.Abstractions.Primitives;

namespace OnlineShop.Application.Errors;
/// <summary>
/// Contains the validation errors.
/// </summary>
internal static class ValidationErrors
{
    /// <summary>
    /// Contains the login errors.
    /// </summary>
    internal static class Login
    {
        internal static Error EmailIsRequired => new Error("Login.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new Error("Login.PasswordIsRequired", "The password is required.");
    }
   
    /// <summary>
    /// Contains the change password errors.
    /// </summary>
    internal static class ChangePassword
    {
        internal static Error UserIdIsRequired => new Error("ChangePassword.UserIdIsRequired", "The user identifier is required.");

        internal static Error PasswordIsRequired => new Error("ChangePassword.PasswordIsRequired", "The password is required.");
    }

    /// <summary>
    /// Contains the create user errors.
    /// </summary>
    internal static class CreateUser
    {
        internal static Error FirstNameIsRequired => new Error("CreateUser.FirstNameIsRequired", "The first name is required.");

        internal static Error LastNameIsRequired => new Error("CreateUser.LastNameIsRequired", "The last name is required.");

        internal static Error EmailIsRequired => new Error("CreateUser.EmailIsRequired", "The email is required.");

        internal static Error PasswordIsRequired => new Error("CreateUser.PasswordIsRequired", "The password is required.");
    }

    /// <summary>
    /// Contains the update user errors.
    /// </summary>
    internal static class UpdateUser
    {
        internal static Error UserIdIsRequired => new Error("UpdateUser.UserIdIsRequired", "The user identifier is required.");

        internal static Error FirstNameIsRequired => new Error("UpdateUser.FirstNameIsRequired", "The first name is required.");

        internal static Error LastNameIsRequired => new Error("UpdateUser.LastNameIsRequired", "The last name is required.");
    }
}

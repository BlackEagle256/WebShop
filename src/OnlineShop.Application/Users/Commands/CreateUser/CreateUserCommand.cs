﻿using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Contracts.Authentication;
using OnlineShop.Domain.Abstractions.Result;

namespace OnlineShop.Application.Users.Commands.CreateUser;
/// <summary>
/// Represents the create user command.
/// </summary>
public sealed class CreateUserCommand : ICommand<Result<TokenResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommand"/> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="email">The email.</param>
    /// <param name="password">The password.</param>
    public CreateUserCommand(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Gets the password.
    /// </summary>
    public string Password { get; }
}

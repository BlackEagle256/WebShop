﻿using FluentValidation;
using OnlineShop.Application.Errors;
using OnlineShop.Application.Extensions;

namespace OnlineShop.Application.Users.Commands.CreateUser;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> validator.
/// </summary>
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
    /// </summary>
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithError(ValidationErrors.CreateUser.FirstNameIsRequired);

        RuleFor(x => x.LastName).NotEmpty().WithError(ValidationErrors.CreateUser.LastNameIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.CreateUser.EmailIsRequired);

        RuleFor(x => x.Password).NotEmpty().WithError(ValidationErrors.CreateUser.PasswordIsRequired);
    }
}
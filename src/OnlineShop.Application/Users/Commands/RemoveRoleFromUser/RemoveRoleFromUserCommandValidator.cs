using FluentValidation;
using OnlineShop.Application.Errors;
using OnlineShop.Application.Extensions;

namespace OnlineShop.Application.Users.Commands.RemoveRoleFromUser;

public sealed class RemoveRoleFromUserCommandValidator:AbstractValidator<RemoveRoleFromUserCommand>
{
    public RemoveRoleFromUserCommandValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().WithError(ValidationErrors.RemoveRoleToUser.UserIdIsRequired);

        RuleFor(e => e.Role).NotEmpty().WithError(ValidationErrors.RemoveRoleToUser.RoleIsRequired);
    }
}
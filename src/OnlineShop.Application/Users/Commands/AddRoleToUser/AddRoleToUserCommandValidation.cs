using FluentValidation;
using OnlineShop.Application.Errors;
using OnlineShop.Application.Extensions;

namespace OnlineShop.Application.Users.Commands.AddRoleToUser;

public sealed class AddRoleToUserCommandValidation:AbstractValidator<AddRoleToUserCommand>
{
    public AddRoleToUserCommandValidation()
    {
        RuleFor(e => e.UserId).NotEmpty().WithError(ValidationErrors.AddRoleToUser.UserIdIsRequired);

        RuleFor(e => e.Role).NotEmpty().WithError(ValidationErrors.AddRoleToUser.RoleIsRequired);
    }
}
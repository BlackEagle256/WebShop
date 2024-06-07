using FluentValidation;
using OnlineShop.Application.Errors;
using OnlineShop.Application.Extensions;

namespace OnlineShop.Application.Roles.Commands.UpdateRole;

public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty().WithError(ValidationErrors.UpdateRole.RoleIdIsRequired);
        RuleFor(e => e.Name).NotEmpty().WithError(ValidationErrors.UpdateRole.NameIsRequired);
    }
}
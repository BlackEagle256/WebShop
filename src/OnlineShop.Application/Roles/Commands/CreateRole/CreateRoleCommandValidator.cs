using FluentValidation;
using OnlineShop.Application.Errors;
using OnlineShop.Application.Extensions;

namespace OnlineShop.Application.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.CreateRole.NameIsRequired);
    }
}
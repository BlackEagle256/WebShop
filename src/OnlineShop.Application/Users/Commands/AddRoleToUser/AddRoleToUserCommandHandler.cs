using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Users.Commands.AddRoleToUser;

internal sealed class AddRoleToUserCommandHandler : ICommandHandler<AddRoleToUserCommand, Result>
{
    private readonly IUserRoleRepositroy _userRoleRepositroy;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;

    public AddRoleToUserCommandHandler(IUserRoleRepositroy userRoleRepositroy, IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRoleRepositroy = userRoleRepositroy;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Role);

        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        var maybeRole = await _roleRepository.GetByNameAsync(nameResult.Value);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }

        var role = maybeRole.Value;

        var isUserInRole = await _userRoleRepositroy.IsUserInRole(user, role);

        if (isUserInRole)
        {
            return Result.Failure(DomainErrors.UserInRole.UserHasRule);
        }

        var userInRole = new UserInRole(user, role);

        _userRoleRepositroy.AddRoleToUser(userInRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

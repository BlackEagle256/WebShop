using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Users.Commands.RemoveRoleFromUser;

internal sealed class RemoveRoleFromUserCommandHandler : ICommandHandler<RemoveRoleFromUserCommand, Result>
{
    private readonly IUserRoleRepositroy _userRoleRepositroy;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;

    public RemoveRoleFromUserCommandHandler(IUserRoleRepositroy userRoleRepositroy, IUnitOfWork unitOfWork, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRoleRepositroy = userRoleRepositroy;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<Result> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
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

        if(!isUserInRole)
        {
            return Result.Failure(DomainErrors.UserInRole.UserHasNoRule);
        }

        _userRoleRepositroy.RemoveRoleFromUser(new UserInRole(user, role));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

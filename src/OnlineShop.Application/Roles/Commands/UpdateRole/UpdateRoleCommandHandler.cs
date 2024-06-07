using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Roles.Commands.UpdateRole;

internal sealed class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, Result>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">The role repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public UpdateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    /// <inheritdoc />
    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);
        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        Maybe<Role> maybeRole = await _roleRepository.GetByIdAsync(request.Id);

        if (maybeRole.HasNoValue)
        {
            return Result.Failure(DomainErrors.Role.NotFound);
        }


        Role role = maybeRole.Value;

        role.Change(nameResult.Value, request.Description?.Trim());

        _roleRepository.Update(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

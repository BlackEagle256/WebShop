using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Roles.Commands.CreateRole;
/// <summary>
/// Represents the <see cref="CreateRoleCommandHandler"/> handler.
/// </summary>
internal sealed class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Result>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateRoleCommandHandler"/> class.
    /// </summary>
    /// <param name="roleRepository">The role repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }
    /// <inheritdoc />
    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Result<Name> nameResult = Name.Create(request.Name);
        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(nameResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure(firstFailureOrSuccess.Error);
        }

        if (await _roleRepository.IsRoleExist(nameResult.Value))
        {
            return Result.Failure(DomainErrors.Role.DuplicateRole);
        }

        var role = Role.Create(nameResult.Value, request.Description?.Trim());

        _roleRepository.Insert(role);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
﻿using OnlineShop.Application.Abstractions.Authentication;
using OnlineShop.Application.Abstractions.Cryptography;
using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Users.Commands.ChangePassword;

/// <summary>
/// Represents the <see cref="ChangePasswordCommand"/> handler.
/// </summary>
internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, Result>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordCommandHandler"/> class.
    /// </summary>
    /// <param name="userIdentifierProvider">The user identifier provider.</param>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="passwordHasher">The password hasher.</param>
    public ChangePasswordCommandHandler(
        IUserIdentifierProvider userIdentifierProvider,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _userIdentifierProvider = userIdentifierProvider;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId != _userIdentifierProvider.UserId)
        {
            return Result.Failure(DomainErrors.User.InvalidPermissions);
        }

        Result<Password> passwordResult = Password.Create(request.Password);

        if (passwordResult.IsFailure)
        {
            return Result.Failure(passwordResult.Error);
        }

        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(DomainErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        string passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        Result result = user.ChangePassword(passwordHash);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
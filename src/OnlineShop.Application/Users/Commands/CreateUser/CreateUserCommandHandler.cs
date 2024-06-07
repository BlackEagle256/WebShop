﻿using OnlineShop.Application.Abstractions.Authentication;
using OnlineShop.Application.Abstractions.Cryptography;
using OnlineShop.Application.Abstractions.Data;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Contracts.Authentication;
using OnlineShop.Domain.Abstractions.Result;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;
using OnlineShop.Domain.ValueObjects;

namespace OnlineShop.Application.Users.Commands.CreateUser;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> handler.
/// </summary>
internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<TokenResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="passwordHasher">The password hasher.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="jwtProvider">The JWT provider.</param>
    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    /// <inheritdoc />
    public async Task<Result<TokenResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<Email> emailResult = Email.Create(request.Email);
        Result<Password> passwordResult = Password.Create(request.Password);

        Result firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult, passwordResult);

        if (firstFailureOrSuccess.IsFailure)
        {
            return Result.Failure<TokenResponse>(firstFailureOrSuccess.Error);
        }

        if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value))
        {
            return Result.Failure<TokenResponse>(DomainErrors.User.DuplicateEmail);
        }

        string passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        var user = User.Create(firstNameResult.Value, lastNameResult.Value, emailResult.Value, passwordHash);

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        string token = _jwtProvider.Create(user);

        return Result.Success(new TokenResponse(token));
    }
}

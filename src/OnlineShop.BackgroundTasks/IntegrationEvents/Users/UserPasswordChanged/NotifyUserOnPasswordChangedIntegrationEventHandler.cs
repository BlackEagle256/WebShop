﻿using OnlineShop.Application.Abstractions.Notifications;
using OnlineShop.Application.Users.Events.PasswordChanged;
using OnlineShop.BackgroundTasks.Abstraction.Messaging;
using OnlineShop.Contracts.Emails;
using OnlineShop.Domain.Abstractions.Exceptions;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;

namespace OnlineShop.BackgroundTasks.IntegrationEvents.Users.UserPasswordChanged;
/// <summary>
/// Represents the <see cref="UserPasswordChangedIntegrationEvent"/> handler.
/// </summary>
internal sealed class NotifyUserOnPasswordChangedIntegrationEventHandler
    : IIntegrationEventHandler<UserPasswordChangedIntegrationEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailNotificationService _emailNotificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotifyUserOnPasswordChangedIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="emailNotificationService">The email notification service.</param>
    public NotifyUserOnPasswordChangedIntegrationEventHandler(
        IUserRepository userRepository,
        IEmailNotificationService emailNotificationService)
    {
        _emailNotificationService = emailNotificationService;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task Handle(UserPasswordChangedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(notification.UserId);

        if (maybeUser.HasNoValue)
        {
            throw new DomainException(DomainErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        var passwordChangedEmail = new PasswordChangedEmail(user.Email, user.FullName);

        await _emailNotificationService.SendPasswordChangedEmail(passwordChangedEmail);
    }
}
using OnlineShop.Application.Abstractions.Notifications;
using OnlineShop.Application.Users.Events.UserCreated;
using OnlineShop.BackgroundTasks.Abstraction.Messaging;
using OnlineShop.Contracts.Emails;
using OnlineShop.Domain.Abstractions.Exceptions;
using OnlineShop.Domain.Abstractions.Maybe;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Errors;
using OnlineShop.Domain.Repositories;

namespace OnlineShop.BackgroundTasks.IntegrationEvents.Users.UserCreated;
/// <summary>
/// Represents the <see cref="UserCreatedIntegrationEvent"/> handler.
/// </summary>
internal sealed class SendWelcomeEmailOnUserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailNotificationService _emailNotificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendWelcomeEmailOnUserCreatedIntegrationEventHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="emailNotificationService">The email notification service.</param>
    public SendWelcomeEmailOnUserCreatedIntegrationEventHandler(
        IUserRepository userRepository,
        IEmailNotificationService emailNotificationService)
    {
        _emailNotificationService = emailNotificationService;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task Handle(UserCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(notification.UserId);

        if (maybeUser.HasNoValue)
        {
            throw new DomainException(DomainErrors.User.NotFound);
        }

        User user = maybeUser.Value;

        var welcomeEmail = new WelcomeEmail(user.Email, user.FullName);

        await _emailNotificationService.SendWelcomeEmail(welcomeEmail);
    }
}
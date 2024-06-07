﻿using OnlineShop.Application.Abstractions.Emails;
using OnlineShop.Application.Abstractions.Notifications;
using OnlineShop.Contracts.Emails;

namespace OnlineShop.Infrastructure.Notifications;
/// <summary>
/// Represents the email notification service.
/// </summary>
internal sealed class EmailNotificationService : IEmailNotificationService
{
    private readonly IEmailService _emailService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailNotificationService"/> class.
    /// </summary>
    /// <param name="emailService">The email service.</param>
    public EmailNotificationService(IEmailService emailService) => _emailService = emailService;

    /// <inheritdoc />
    public async Task SendWelcomeEmail(WelcomeEmail welcomeEmail)
    {
        var mailRequest = new MailRequest(
            welcomeEmail.EmailTo,
            "Welcome to OnlineShop! 🎉",
            $"Welcome to OnlineShop {welcomeEmail.Name}," +
            Environment.NewLine +
            Environment.NewLine +
            $"You have registered with the email {welcomeEmail.EmailTo}.");

        await _emailService.SendEmailAsync(mailRequest);
    }

    /// <inheritdoc />
    public async Task SendPasswordChangedEmail(PasswordChangedEmail passwordChangedEmail)
    {
        var mailRequest = new MailRequest(
            passwordChangedEmail.EmailTo,
            "Password changed 🔐",
            $"Hello {passwordChangedEmail.Name}," +
            Environment.NewLine +
            Environment.NewLine +
            "Your password was successfully changed.");

        await _emailService.SendEmailAsync(mailRequest);
    }

    /// <inheritdoc />
    public async Task SendNotificationEmail(NotificationEmail notificationEmail)
    {
        var mailRequest = new MailRequest(notificationEmail.EmailTo, notificationEmail.Subject, notificationEmail.Body);

        await _emailService.SendEmailAsync(mailRequest);
    }
}
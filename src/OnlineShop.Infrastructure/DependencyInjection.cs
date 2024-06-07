using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Application.Abstractions.Authentication;
using OnlineShop.Application.Abstractions.Common;
using OnlineShop.Application.Abstractions.Cryptography;
using OnlineShop.Application.Abstractions.Emails;
using OnlineShop.Application.Abstractions.Messaging;
using OnlineShop.Application.Abstractions.Notifications;
using OnlineShop.Domain.Services;
using OnlineShop.Infrastructure.Authentication;
using OnlineShop.Infrastructure.Authentication.Settings;
using OnlineShop.Infrastructure.Common;
using OnlineShop.Infrastructure.Cryptography;
using OnlineShop.Infrastructure.Emails;
using OnlineShop.Infrastructure.Emails.Settings;
using OnlineShop.Infrastructure.Messaging;
using OnlineShop.Infrastructure.Messaging.Settings;
using OnlineShop.Infrastructure.Notifications;
using System.Text;

namespace OnlineShop.Infrastructure;
public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"]!))
            });

        //services.AddAuthorization(config =>
        //{
        //    config.AddPolicy(,x => x.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme));
        //});

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SettingsKey));

        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SettingsKey));

        services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.SettingsKey));

        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddTransient<IDateTime, MachineDateTime>();

        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddTransient<IPasswordHashChecker, PasswordHasher>();

        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IEmailNotificationService, EmailNotificationService>();

        services.AddSingleton<IIntegrationEventPublisher, IntegrationEventPublisher>();

        return services;
    }

}

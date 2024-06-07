using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.BackgroundTasks.Services;
using OnlineShop.BackgroundTasks.Settings;
using OnlineShop.BackgroundTasks.Tasks;
using System.Reflection;

namespace OnlineShop.BackgroundTasks;
public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config=>config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<BackgroundTaskSettings>(configuration.GetSection(BackgroundTaskSettings.SettingsKey));

        services.AddHostedService<IntegrationEventConsumerBackgroundService>();

        services.AddScoped<IIntegrationEventConsumer, IntegrationEventConsumer>();

        return services;
    }
}
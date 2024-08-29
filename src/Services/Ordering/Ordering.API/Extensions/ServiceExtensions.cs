using Infrastructure.Configuarations;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.Configurations;
using Infrastructure.Extensions;
using MassTransit;
using Ordering.API.Application.IntegrationEvents.EventsHandler;
namespace Ordering.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(EmailSMTPSettings)).Get<EmailSMTPSettings>();

            services.AddSingleton(emailSettings); // duy truy xuyên suốt ứng dụng

            var eventBusSettings = configuration.GetSection(nameof(EventBusSettings)).Get<EventBusSettings>();

            services.AddSingleton(eventBusSettings);
            return services;
        }

        public static void ConfigureMassTransit(this IServiceCollection services)
        {
            var settings = services.GetOptions<EventBusSettings>("EventBusSettings");
            if (string.IsNullOrEmpty(settings.HostAddress))
            {
                throw new ArgumentNullException("EventBusSettings is not configured.");
            }

            var mqConnection = new Uri(settings.HostAddress);
            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(config =>
            {
                //add consume
                config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(mqConnection);
                    cfg.ReceiveEndpoint("basket-checkout-queue", c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutEventHandler>(ctx);
                    });

                    //co the viet theo receive nhu tren, hoac viet nhu sau de bat ki thang nao co trien khai IConsumer se deu trien khai
                    //  cfg.ConfigureEndpoints(ctx);
                });
            });
        }

        public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnectionString"), name: "SqlServer Health", failureStatus: HealthStatus.Degraded);
        }
    }
}
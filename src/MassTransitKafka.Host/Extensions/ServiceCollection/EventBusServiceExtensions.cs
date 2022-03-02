using Confluent.Kafka;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransitKafka.Bus.Consumer;
using MassTransitKafka.DomainEvent;
using MassTransitKafka.EventBus;
using MassTransitKafka.Host.Configurations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusServiceExtensions
    {
        public static IServiceCollection AddDistributedBus(this IServiceCollection services,
            IConfiguration configuration)
        {
            var eventBusConfiguration = configuration
                .GetSection(EventBusConfiguration.EventBus)
                .Get<EventBusConfiguration>();

            services.AddScoped<IDistributedBus, DistributedBus>();
            services.AddMassTransit(s => s.AddKafka(eventBusConfiguration));
            services.AddMassTransitHostedService();
            return services;
        }

        private static void AddKafka(this IServiceCollectionBusConfigurator services, EventBusConfiguration configuration)
        {
            services.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

            services.AddRider(rider =>
            {
                rider.AddConsumer<PaymentConsumer>();

                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host(configuration.Host);

                    cfg.TopicEndpoint<Null, PaymentEvent>(nameof(PaymentEvent), nameof(MassTransitKafka), e =>
                    {
                        e.ConfigureConsumer<PaymentConsumer>(context);
                    });
                });
            });
        }
    }
}

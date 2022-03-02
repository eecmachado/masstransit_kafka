using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.KafkaIntegration;
using MassTransitKafka_Payment.Bus.Consumer;
using MassTransitKafka_Payment.EventBus;
using MassTransitKafka_Payment.Host.Configurations;
using MassTransitKafka_Payment.DomainEvent;
using System.Net;
using System.Reflection;

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
            services.AddMassTransitHostedService(true);
            return services;
        }

        private static void AddKafka(this IServiceCollectionBusConfigurator services, EventBusConfiguration configuration)
        {
            services.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

            services.AddRider(rider =>
            {
                rider.AddConsumer<CancellationConsumer>();
                rider.AddProducer<PaymentEvent>(nameof(PaymentEvent));

                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host(configuration.Host);

                    cfg.TopicEndpoint<CancellationEvent>(nameof(CancellationEvent), GetUniqueName(nameof(CancellationEvent)), e =>
                    {
                        e.CheckpointInterval = TimeSpan.FromSeconds(10);
                        e.ConfigureConsumer<CancellationConsumer>(context);
                        e.CreateIfMissing();
                    });
                });
            });
        }

        private static string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }
    }
}

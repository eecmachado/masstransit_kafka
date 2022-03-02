using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.KafkaIntegration;
using MassTransitKafka_Cancellation.Bus.Consumer;
using MassTransitKafka_Cancellation.Cancellation.DomainEvent;
using MassTransitKafka_Cancellation.EventBus;
using MassTransitKafka_Cancellation.Host.Configurations;
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

            services.AddScoped<IDistributedBus<CancellationEvent>, DistributedBus<CancellationEvent>>();
            services.AddMassTransit(s => s.AddKafka(eventBusConfiguration));
            services.AddMassTransitHostedService(true);
            return services;
        }

        private static void AddKafka(this IServiceCollectionBusConfigurator services, EventBusConfiguration configuration)
        {
            services.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

            services.AddRider(rider =>
            {
                rider.AddConsumer<PaymentConsumer>();
                rider.AddProducer<CancellationEvent>(nameof(CancellationEvent));

                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host(configuration.Host);

                    cfg.TopicEndpoint<PaymentEvent>(nameof(PaymentEvent), GetUniqueName(nameof(PaymentEvent)), e =>
                    {
                        e.CheckpointInterval = TimeSpan.FromSeconds(10);
                        e.ConfigureConsumer<PaymentConsumer>(context);
                        e.CreateIfMissing(t => { });
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

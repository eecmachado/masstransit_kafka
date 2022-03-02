using MassTransit;
using MassTransit.Definition;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransitKafka.EventBus;
using MassTransitKafka.Host.Configurations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusServiceExtensions
    {
        public static IServiceCollection AddDistributedBus(this IServiceCollection services,
            IConfiguration eventBusConfiguration)
        {
            var configuration = eventBusConfiguration.GetSection("").Get<EventBusConfiguration>();

            services.AddScoped<IDistributedBus, DistributedBus>();
            services.AddMassTransit(s => s.AddKafka());

            //var formatter = new KebabCaseEndpointNameFormatter(configuration.Prefix, false);
            //services.AddSingleton<IEndpointNameFormatter>(formatter);
            services.AddMassTransitHostedService();
            return services;
        }

        private static void AddKafka(this IServiceCollectionBusConfigurator services)
        {
            services.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

            services.AddRider(rider =>
            {
                //rider.AddConsumers(consumersAssemblies);
                rider.UsingKafka((context, cfg) =>
                {
                    cfg.Host("localhost:9092");

                    //k.TopicEndpoint<KafkaMessage>("topic-name", "consumer-group-name", e =>
                    //{
                    //    e.ConfigureConsumer<KafkaMessageConsumer>(context);
                    //});
                });
            });
        }
    }
}

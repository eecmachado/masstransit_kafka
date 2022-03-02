namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCorrelationId(configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDistributedBus(configuration);
            return services;
        }
    }
}

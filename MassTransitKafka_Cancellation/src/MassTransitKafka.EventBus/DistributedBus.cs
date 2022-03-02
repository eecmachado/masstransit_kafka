using MassTransit;

namespace MassTransitKafka_Cancellation.EventBus
{
    public class DistributedBus : IDistributedBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public DistributedBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Publish<T>(T message, CancellationToken cancellationToken = default)
            where T : class
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
    }
}

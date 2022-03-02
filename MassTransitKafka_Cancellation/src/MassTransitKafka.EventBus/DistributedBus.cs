using MassTransit.KafkaIntegration;

namespace MassTransitKafka_Cancellation.EventBus
{
    public class DistributedBus<T> : IDistributedBus<T>
        where T : class
    {
        private readonly ITopicProducer<T> _bus;

        public DistributedBus(ITopicProducer<T> bus)
        {
            _bus = bus;
        }

        public Task Produce(T message, CancellationToken cancellationToken)
        {
            _bus.Produce(message, cancellationToken);
            return Task.CompletedTask;
        }
    }
}

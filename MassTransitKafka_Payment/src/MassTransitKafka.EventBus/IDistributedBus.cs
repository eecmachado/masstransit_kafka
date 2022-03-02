namespace MassTransitKafka_Payment.EventBus
{
    public interface IDistributedBus
    {
        Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}

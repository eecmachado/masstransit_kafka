namespace MassTransitKafka_Cancellation.EventBus
{
    public interface IDistributedBus
    {
        Task Publish<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}

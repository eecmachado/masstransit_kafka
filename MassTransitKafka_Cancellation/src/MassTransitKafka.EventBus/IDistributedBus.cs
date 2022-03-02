namespace MassTransitKafka_Cancellation.EventBus
{
    public interface IDistributedBus<T>
        where T : class
    {
        Task Produce(T message, CancellationToken cancellationToken = default);
    }
}

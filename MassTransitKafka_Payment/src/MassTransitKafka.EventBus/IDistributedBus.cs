namespace MassTransitKafka_Payment.EventBus
{
    public interface IDistributedBus<T>
        where T : class
    {
        Task Produce(T message, CancellationToken cancellationToken);
    }
}

using Automatonymous;
using MassTransit;

namespace MassTransitKafka.EventBus
{
    public class DistributedBus : IDistributedBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public DistributedBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Publish<T>(T message, CancellationToken cancellationToken = default)
            where T : class
        {
            return _publishEndpoint.Publish(message, cancellationToken);
        }
    }

    //public class OrderState : SagaStateMachineInstance
    //{
    //    public Guid CorrelationId { get; set; }
    //    public int CurrentState { get; set; }
    //}

    //public interface OrderAccepted
    //{
    //    Guid OrderId { get; }
    //}

    //public class OrderStateMachine :
    //    MassTransitStateMachine<OrderState>
    //{

    //    public State Submitted { get; private set; }
    //    public State Accepted { get; private set; }

    //    public OrderStateMachine()
    //    {
    //        Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));

    //        During(Submitted,
    //            When(OrderAccepted)
    //                .TransitionTo(Accepted));
    //    }

    //    public Event<OrderAccepted> OrderAccepted { get; private set; }
    //}
}

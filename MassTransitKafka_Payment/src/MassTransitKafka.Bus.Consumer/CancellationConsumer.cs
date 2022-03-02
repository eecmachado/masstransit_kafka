using MassTransit;
using MassTransitKafka_Payment.DomainEvent;
using Microsoft.Extensions.Logging;

namespace MassTransitKafka_Payment.Bus.Consumer
{
    public class CancellationConsumer : IConsumer<CancellationEvent>
    {
        private readonly ILogger<CancellationConsumer> _logger;

        public CancellationConsumer(ILogger<CancellationConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<CancellationEvent> context)
        {
            _logger.LogInformation($"Cancellation received. Id: {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}
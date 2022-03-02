﻿using MassTransit;
using MassTransitKafka.DomainEvent;
using Microsoft.Extensions.Logging;

namespace MassTransitKafka.Bus.Consumer
{
    public class PaymentConsumer : IConsumer<PaymentEvent>
    {
        private readonly ILogger<PaymentConsumer> _logger;

        public PaymentConsumer(ILogger<PaymentConsumer> logger) => _logger = logger;

        public Task Consume(ConsumeContext<PaymentEvent> context)
        {
            _logger.LogInformation($"Payment received. Id: {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}
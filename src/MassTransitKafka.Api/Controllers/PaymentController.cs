using MassTransitKafka.DomainEvent;
using MassTransitKafka.EventBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitKafka.Api.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IDistributedBus _bus;

        public PaymentController(IDistributedBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] PaymentEvent request, CancellationToken cancellationToken = default)
        {
            await _bus.Publish(request, cancellationToken);
            return Ok(request);
        }
    }
}

using MassTransitKafka_Cancellation.Cancellation.DomainEvent;
using MassTransitKafka_Cancellation.EventBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitKafka_Cancellation.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CancellationController : ControllerBase
    {
        private readonly IDistributedBus _bus;

        public CancellationController(IDistributedBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CancellationEvent), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CancellationEvent request, CancellationToken cancellationToken = default)
        {
            await _bus.Publish(request, cancellationToken);
            return Ok(request);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MassTransitKafka_Cancellation.Host.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index() => new RedirectResult("~/swagger");
    }
}

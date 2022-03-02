using Microsoft.AspNetCore.Mvc;

namespace MassTransitKafka.Host.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index() => new RedirectResult("~/swagger");
    }
}

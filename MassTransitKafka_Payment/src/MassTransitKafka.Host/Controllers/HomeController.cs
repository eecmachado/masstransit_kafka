using Microsoft.AspNetCore.Mvc;

namespace MassTransitKafka_Payment.Host.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index() => new RedirectResult("~/swagger");
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SCAGEUsers.api.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}

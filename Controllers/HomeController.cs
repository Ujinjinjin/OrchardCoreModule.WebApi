using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.WebApi.Controllers
{
    public class HomeController : Controller
    {
        [Route("api/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/test")]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}

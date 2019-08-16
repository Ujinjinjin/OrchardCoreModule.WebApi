using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.WebApi.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        [Route("api/admin/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/admin/test")]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}

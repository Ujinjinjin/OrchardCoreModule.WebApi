using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.WebApi.Controllers
{
    [Route("admin/webapi")]
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace OrchardCore.WebApi.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet, Route("admin/webapi")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet, Route("api/getsomething")]
        public ActionResult<IEnumerable<string>> GetSomething()
        {
            return new string[] {"value1", "value2"};
        }
    }
}
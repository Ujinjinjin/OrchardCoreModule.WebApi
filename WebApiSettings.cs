using System;
using Microsoft.AspNetCore.Http;

namespace OrchardCore.WebApi
{
    public class WebApiSettings
    {
        public PathString Path { get; set; } = "/api/webapi";
        public Func<HttpContext, object> BuildUserContext { get; set; }
        public bool ExposeExceptions = false;
    }
}
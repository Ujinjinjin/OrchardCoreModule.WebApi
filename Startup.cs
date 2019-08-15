using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;

namespace OrchardCore.WebApi
{
    public class Startup : StartupBase
    {
        private readonly IShellConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IShellConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            
        }

        public override void Configure(IApplicationBuilder app, IRouteBuilder routes, IServiceProvider serviceProvider)
        {
            var exposeExceptions = _configuration.GetValue(
                $"OrchardCore.Apis.WebApi:{nameof(WebApiSettings.ExposeExceptions)}",
                _hostingEnvironment.IsDevelopment());

//            app.UseMiddleware<WebApiMiddleware>();
        }
    }
}

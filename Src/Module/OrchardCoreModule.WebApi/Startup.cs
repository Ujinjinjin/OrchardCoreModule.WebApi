using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Modules;
using OrchardCoreModule.WebApi.Factories;
using OrchardCoreModule.WebApi.Logger;
using OrchardCoreModule.WebApi.Repository;

namespace OrchardCoreModule.WebApi
{
	public class Startup : StartupBase
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IDatabaseContextFactory, DatabaseContextFactory>();
			services.AddTransient<ICmsRepository, CmsRepository>();
			services.AddTransient<ILogger, ConsoleLogger>();
		}
	}
}
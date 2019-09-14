using System;
using Common.Db.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Repository;
using OrchardCoreModule.WebApi.Repository.DbContext;

namespace OrchardCoreModule.WebApi
{
	public class Startup : StartupBase
	{
		private readonly IShellConfiguration _configuration;
		private readonly IHostingEnvironment _hostingEnvironment;

		public Startup(IShellConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
		}

		public override void ConfigureServices(IServiceCollection services)
		{
			ConfigureRepository(services);
		}

		public override void Configure(IApplicationBuilder builder, IRouteBuilder routes, IServiceProvider serviceProvider)
		{
			builder.UseMvc();
		}

		private void ConfigureRepository(IServiceCollection services)
		{
			DatabaseType databaseType;
			if (int.TryParse(Environment.GetEnvironmentVariable(EnvironmentVariable.DatabaseType), out var dbType))
			{
				databaseType = (DatabaseType) dbType;
			}
			else
			{
				throw new ArgumentNullException($"Please provide database type of your system");
			}

			switch (databaseType)
			{
				case DatabaseType.SqlServer:
					services.AddTransient<ICmsRepository, SqlServerCmsRepository>();
					break;
				case DatabaseType.Postgres:
				case DatabaseType.MySql:
				case DatabaseType.SqLite:
				default:
					throw new ArgumentOutOfRangeException();
			}	
		}
	}
}
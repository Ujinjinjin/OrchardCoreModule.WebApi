using System;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Modules;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Repository;

namespace OrchardCoreModule.WebApi
{
	public class Startup : StartupBase
	{
		public override void ConfigureServices(IServiceCollection services)
		{
			ConfigureRepository(services);
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
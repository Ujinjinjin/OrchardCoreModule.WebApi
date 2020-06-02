using LinqToDB;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Repository.DbContext;
using System;

namespace OrchardCoreModule.WebApi.Factories
{
	internal class DatabaseContextFactory : IDatabaseContextFactory
	{
		private readonly string _providerName;
		private readonly string _connectionString;
		
		public DatabaseContextFactory()
		{
			_providerName = Environment.GetEnvironmentVariable(EnvironmentVariable.ProviderName);
			_connectionString = Environment.GetEnvironmentVariable(EnvironmentVariable.CmsConnectionString);

			if (string.IsNullOrWhiteSpace(_providerName))
			{
				throw new ArgumentNullException(nameof(_providerName));
			}
			if (string.IsNullOrWhiteSpace(_connectionString))
			{
				throw new ArgumentNullException(nameof(_connectionString));
			}
		}
		
		public ICmsDbContext CreateDatabaseContext()
		{
			switch (_providerName)
			{
				case ProviderName.PostgreSQL95:
				case ProviderName.SqlServer2017:
					return new CmsDatabaseContext(_providerName, _connectionString);
				default:
					throw new ArgumentOutOfRangeException(_providerName);
			}
		}
	}
}
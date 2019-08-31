using System.Collections.Generic;
using System.Linq;
using LinqToDB.Configuration;

namespace CommonUtils.Db
{
	internal class SqlServerConnectionSettings : ILinqToDBSettings
	{
		public SqlServerConnectionSettings(string connectionString)
		{
			_connectionString = connectionString;
		}

		private readonly string _connectionString;
		
		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
		public string DefaultConfiguration => "SqlServer";
		public string DefaultDataProvider => "SqlServer";
		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				yield return 
					new ConnectionStringSetting
					{
						Name = "SqlServer",
						ProviderName = "SqlServer",
						ConnectionString = _connectionString
					};
			}
		}
	}
}
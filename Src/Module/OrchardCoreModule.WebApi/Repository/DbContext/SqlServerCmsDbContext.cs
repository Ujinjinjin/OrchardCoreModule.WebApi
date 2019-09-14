using System;
using System.Collections.Generic;
using System.Linq;
using Common.Db;
using LinqToDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using OrchardCoreModule.WebApi.Const;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	internal class SqlServerCmsDbContext : DataConnectionBase, ICmsDbContext
	{
		private static readonly ILogger Logger = new DebugLogger(nameof(SqlServerCmsDbContext));

		protected internal SqlServerCmsDbContext()
			: base(ProviderName.SqlServer2017, Environment.GetEnvironmentVariable(EnvironmentVariable.CmsConnectionString), Logger)
		{
		}

		public IList<int> GetStuff()
		{
			var result = Query<int>("cms__get_stuff").ToList();
			return result;
		}
	}
}
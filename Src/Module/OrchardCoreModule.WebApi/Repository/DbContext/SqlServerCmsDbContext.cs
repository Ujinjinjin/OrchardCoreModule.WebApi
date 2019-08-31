using System;
using System.Collections.Generic;
using System.Linq;
using Common.Db;
using Common.Db.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using OrchardCoreModule.WebApi.Const;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	internal class SqlServerCmsDbContext : DataConnectionBase, ICmsDbContext
	{
		private static readonly ILogger Logger = new DebugLogger(nameof(SqlServerCmsDbContext));

		protected SqlServerCmsDbContext()
			: base(DatabaseType.SqlServer, Environment.GetEnvironmentVariable(EnvironmentVariable.CmsConnectionString), Logger)
		{
		}

		public IList<int> GetStuff()
		{
			var result = Query<int>("cms__get_stuff").ToList();
			return result;
		}
	}
}
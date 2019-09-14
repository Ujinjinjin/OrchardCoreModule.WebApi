using System;
using System.Collections.Generic;
using System.Linq;
using Common.Db;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Repository.DbClasses;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	/// <inheritdoc cref="ICmsRepository" />
	internal class SqlServerCmsDbContext : DataConnectionBase, ICmsDbContext
	{
		private static readonly ILogger Logger = new DebugLogger(nameof(SqlServerCmsDbContext));

		protected internal SqlServerCmsDbContext()
			: base(ProviderName.SqlServer2017, Environment.GetEnvironmentVariable(EnvironmentVariable.CmsConnectionString), Logger)
		{
		}

		public IList<DbContentItemIndex> GetContentItemList(string contentType, bool? published)
		{
			return Query<DbContentItemIndex>(
				"cms__get_content_list",
				new DataParameter("p_content_type", contentType),
				new DataParameter("p_published", published)
			).ToList();
		}

		public DbContentItemIndex GetContentItemById(string contentItemId)
		{
			return Query<DbContentItemIndex>(
				"cms__get_content_by_id",
				new DataParameter("p_content_item_id", contentItemId)
			).Single();
		}
	}
}
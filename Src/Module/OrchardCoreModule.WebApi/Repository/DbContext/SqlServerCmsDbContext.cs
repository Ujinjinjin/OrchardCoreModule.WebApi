using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Logger;
using OrchardCoreModule.WebApi.Repository.DbClasses;
using OrchardCoreModule.WebApi.Utils.Database;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	/// <inheritdoc cref="ICmsRepository" />
	internal class SqlServerCmsDbContext : DataConnectionBase, ICmsDbContext
	{
		private static readonly ILogger Logger = new ConsoleLogger();

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

		public DbContentItemIndex GetContentItemById(string contentType, string contentItemId)
		{
			return Query<DbContentItemIndex>(
				"cms__get_content_by_id",
				new DataParameter("p_content_type", contentType),
				new DataParameter("p_content_item_id", contentItemId)
			)
				.OrderBy(x => x.Published)
				.LastOrDefault();
		}
	}
}
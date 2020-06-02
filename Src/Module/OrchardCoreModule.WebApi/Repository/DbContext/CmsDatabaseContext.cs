using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Logger;
using OrchardCoreModule.WebApi.Repository.DbClasses;
using OrchardCoreModule.WebApi.Utils.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	/// <inheritdoc cref="ICmsRepository" />
	internal class CmsDatabaseContext : DataConnectionBase, ICmsDbContext
	{
		private static readonly ILogger Logger = new ConsoleLogger();

		protected internal CmsDatabaseContext(string providerName, string connectionString)
			: base(providerName, connectionString, Logger)
		{
		}

		public async Task<IList<DbContentItemIndex>> GetContentItemListAsync(
			string contentType,
			DateTime? dateFrom,
			bool? published,
			bool? isDeleted)
		{
			var result = await QueryAsync<DbContentItemIndex>(
				"cms__get_content_list",
				new DataParameter("p_content_type", contentType),
				new DataParameter("p_date_from", dateFrom),
				new DataParameter("p_published", published),
				new DataParameter("p_is_deleted", isDeleted)
			);
			return result.ToList();
		}

		public async Task<DbContentItemIndex> GetContentItemByIdAsync(
			string contentType,
			string contentItemId,
			bool? published)
		{
			var result = await QueryAsync<DbContentItemIndex>(
				"cms__get_content_by_id",
				new DataParameter("p_content_type", contentType),
				new DataParameter("p_content_item_id", contentItemId),
				new DataParameter("p_published", published)
			);
			return result.SingleOrDefault();
		}
	}
}
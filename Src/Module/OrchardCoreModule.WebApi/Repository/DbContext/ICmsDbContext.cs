using OrchardCoreModule.WebApi.Repository.DbClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	internal interface ICmsDbContext : IDisposable
	{
		/// <summary> Get list of content items by filter </summary>
		Task<IList<DbContentItemIndex>> GetContentItemListAsync(
			string contentType,
			DateTime? dateFrom,
			bool? published,
			bool? isDeleted);

		/// <summary> Get content item by id </summary>
		Task<DbContentItemIndex> GetContentItemByIdAsync(string contentType, string contentItemId, bool? published);
	}
}
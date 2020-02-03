using System.Collections.Generic;
using OrchardCoreModule.WebApi.Repository.DbClasses;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	internal interface ICmsDbContext
	{
		/// <summary> Get list of content items by filter </summary>
		IList<DbContentItemIndex> GetContentItemList(string contentType, bool? published);

		/// <summary> Get content item by id </summary>
		DbContentItemIndex GetContentItemById(string contentType, string contentItemId);
	}
}
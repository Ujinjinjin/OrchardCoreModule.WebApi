using System.Collections.Generic;
using OrchardCoreModule.WebApi.Abstractions;

namespace OrchardCoreModule.WebApi.Repository
{
	public interface ICmsRepository
	{
		/// <summary> Get list of content items by filter </summary>
		IList<ContentItemIndex> GetContentItemList(string contentType, bool? published);
		
		/// <summary> Get content item by id </summary>
		ContentItemIndex GetContentItemById(string contentItemId);
	}
}
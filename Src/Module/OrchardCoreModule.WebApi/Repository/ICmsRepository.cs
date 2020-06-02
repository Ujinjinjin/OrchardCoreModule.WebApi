using OrchardCoreModule.WebApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Repository
{
	public interface ICmsRepository
	{
		/// <summary> Get list of content items by filter </summary>
		Task<IList<ContentItemIndex<T>>> GetContentItemListAsync<T>(
			string contentType,
			DateTime? dateFrom,
			bool? published,
			bool? isDeleted
		);

		/// <summary> Get content item by id </summary>
		Task<ContentItemIndex<T>> GetContentItemByIdAsync<T>(string contentType, string contentItemId, bool? published);
	}
}
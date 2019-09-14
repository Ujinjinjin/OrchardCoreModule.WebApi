using System;
using System.Collections.Generic;
using System.Linq;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Converters;
using OrchardCoreModule.WebApi.Repository.DbContext;

namespace OrchardCoreModule.WebApi.Repository
{
	/// <inheritdoc />
	internal class SqlServerCmsRepository : ICmsRepository
	{
		public IList<ContentItemIndex> GetContentItemList(string contentType, bool? published)
		{
			using (var dbContext = new SqlServerCmsDbContext())
			{
				return dbContext.GetContentItemList(contentType, published)
					.Select(item => item.ToInterface())
					.ToArray();	
			}
		}

		public ContentItemIndex GetContentItemById(string contentItemId)
		{
			using (var dbContext = new SqlServerCmsDbContext())
			{
				return dbContext.GetContentItemById(contentItemId).ToInterface();
			}
		}
	}
}
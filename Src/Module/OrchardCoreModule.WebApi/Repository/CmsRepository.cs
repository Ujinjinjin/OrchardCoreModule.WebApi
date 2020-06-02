using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Converters;
using OrchardCoreModule.WebApi.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Repository
{
	/// <inheritdoc />
	internal class CmsRepository : ICmsRepository
	{
		private readonly IDatabaseContextFactory _databaseContextFactory;

		public CmsRepository(IDatabaseContextFactory databaseContextFactory)
		{
			_databaseContextFactory = databaseContextFactory ?? throw new ArgumentNullException(nameof(databaseContextFactory));
		}

		public async Task<IList<ContentItemIndex<T>>> GetContentItemListAsync<T>(
			string contentType,
			DateTime? dateFrom,
			bool? published,
			bool? isDeleted)
		{
			using (var dbContext = _databaseContextFactory.CreateDatabaseContext())
			{
				var result = await dbContext.GetContentItemListAsync(contentType, dateFrom, published, isDeleted);
				return result
					.Select(item => item.ToInterface<T>())
					.ToArray();	
			}
		}

		public async Task<ContentItemIndex<T>> GetContentItemByIdAsync<T>(
			string contentType,
			string contentItemId,
			bool? published)
		{
			using (var dbContext = _databaseContextFactory.CreateDatabaseContext())
			{
				var result = await dbContext.GetContentItemByIdAsync(contentType, contentItemId, published);
				return result.ToInterface<T>();
			}
		}
	}
}
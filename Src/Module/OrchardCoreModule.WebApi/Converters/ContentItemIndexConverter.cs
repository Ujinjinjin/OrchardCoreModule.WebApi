using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Repository.DbClasses;

namespace OrchardCoreModule.WebApi.Converters
{
	internal static class ContentItemIndexConverter
	{
		public static ContentItemIndex ToInterface(this DbContentItemIndex source)
		{
			return new ContentItemIndex
			{
				Id = source.Id,
				ContentItemId = source.ContentItemId,
				Latest = source.Latest,
				Published = source.Published,
				ContentType = source.ContentType,
				DocumentId = source.DocumentId,
				Content = source.Content,
			};
		}
		
		public static DbContentItemIndex ToDb(this ContentItemIndex source)
		{
			return new DbContentItemIndex
			{
				Id = source.Id,
				ContentItemId = source.ContentItemId,
				Latest = source.Latest,
				Published = source.Published,
				ContentType = source.ContentType,
				DocumentId = source.DocumentId,
				Content = source.Content,
			};
		}
	}
}
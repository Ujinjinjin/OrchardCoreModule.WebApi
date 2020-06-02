using Newtonsoft.Json;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Repository.DbClasses;

namespace OrchardCoreModule.WebApi.Converters
{
	internal static class ContentItemIndexConverter
	{
		public static ContentItemIndex<T> ToInterface<T>(this DbContentItemIndex source)
		{
			if (source == null)
			{
				return null;
			}
			
			return new ContentItemIndex<T>
			{
				Id = source.Id,
				ContentItemId = source.ContentItemId,
				Latest = source.Latest,
				Published = source.Published,
				ContentType = source.ContentType,
				DocumentId = source.DocumentId,
				Content = JsonConvert.DeserializeObject<T>(source.Content),
			};
		}
	}
}
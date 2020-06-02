using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Models;

namespace OrchardCoreModule.WebApi.Converters
{
	internal static class CallbackConverter
	{
		public static CallbackData ToInterface(this CallbackInfo source)
		{
			if (source == null)
			{
				return null;
			}
			
			return new CallbackData
			{
				Action = (CallbackAction)source.Callback.Action.Value,
				ContentType = source.Callback.ContentType.Text,
				ContentItemId = source.Callback.ContentItemId.Text
			};
		}
	}
}
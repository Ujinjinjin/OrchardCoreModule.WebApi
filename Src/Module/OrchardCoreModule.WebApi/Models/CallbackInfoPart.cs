using OrchardCoreModule.WebApi.Models.ContentParts;
using System.Net.Mime;

namespace OrchardCoreModule.WebApi.Models
{
	internal class CallbackPart
	{
		public TextPart ContentType { get; set; }
		public TextPart ContentItemId { get; set; }
		public BooleanPart Published { get; set; }
		public NumericPart Action { get; set; }
	}
}
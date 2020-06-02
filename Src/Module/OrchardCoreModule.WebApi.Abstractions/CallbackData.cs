using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	public class CallbackData
	{
		/// <summary> Content Item Id </summary>
		[DataMember]
		public string ContentItemId { get; set; }

		/// <summary> Content Type </summary>
		[DataMember]
		public string ContentType { get; set; }

		/// <summary> Действие </summary>
		[DataMember]
		public CallbackAction Action { get; set; }
	}
}
using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	[DataContract]
	public class GetContentItemRequest
	{
		/// <summary> ID of an content item </summary>
		[DataMember]
		public string Id { get; set; }
		
		/// <summary> Content type </summary>
		[DataMember]
		public string ContentType { get; set; }

		/// <summary> Publishing flag </summary>
		[DataMember]
		public bool? Published { get; set; }
	}
}
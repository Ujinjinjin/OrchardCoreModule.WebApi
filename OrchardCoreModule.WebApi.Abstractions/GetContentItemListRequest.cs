using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	[DataContract]
	public class GetContentItemListRequest
	{
		/// <summary> Technical name of an contract type in CMS </summary>
		[DataMember]
		public string ContentType { get; set; }
	}
}
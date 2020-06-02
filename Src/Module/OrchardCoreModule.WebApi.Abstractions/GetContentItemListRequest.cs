using System;
using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	[DataContract]
	public class GetContentItemListRequest
	{
		/// <summary> Technical name of an contract type in CMS </summary>
		[DataMember]
		public string ContentType { get; set; }

		/// <summary> Date From </summary>
		[DataMember]
		public DateTime? DateFrom { get; set; }

		/// <summary> Publishing flag </summary>
		[DataMember]
		public bool? Published { get; set; }
		
		/// <summary> Deletion flag </summary>
		[DataMember]
		public bool? IsDeleted { get; set; }
	}
}
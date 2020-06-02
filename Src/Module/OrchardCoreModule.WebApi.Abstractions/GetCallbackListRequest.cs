using System;
using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	public class GetCallbackListRequest
	{
		/// <summary> Date From </summary>
		[DataMember]
		public DateTime? DateFrom { get; set; }

		/// <summary> Publishing flag </summary>
		[DataMember]
		public bool? Published { get; set; }
	}
}
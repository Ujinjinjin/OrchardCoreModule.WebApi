using System.Runtime.Serialization;

namespace OrchardCoreModule.WebApi.Abstractions
{
	/// <summary> Diff action </summary>
	[DataContract]
	public enum CallbackAction
	{
		/// <summary> Unknown </summary>
		[EnumMember] Unknown = 0,

		/// <summary> Update content </summary>
		[EnumMember] Update = 1,

		/// <summary> Delete content </summary>
		[EnumMember] Delete = 2
	}
}
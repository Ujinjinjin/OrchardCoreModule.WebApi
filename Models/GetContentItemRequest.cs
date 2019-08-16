using System.Runtime.Serialization;

namespace OrchardCore.WebApi.Models
{
    [DataContract]
    public class GetContentItemRequest
    {
        /// <summary> ID of an content item </summary>
        [DataMember]
        public string Id { get; set; }
    }
}
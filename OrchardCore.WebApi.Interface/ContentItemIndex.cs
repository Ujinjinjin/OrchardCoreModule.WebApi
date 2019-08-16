using System.Runtime.Serialization;

namespace OrchardCore.WebApi.Interface
{
    /// <summary> Content index item </summary>
    [DataContract]
    public class ContentItemIndex<T>
    {
        /// <summary> Content index ID </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary> Content item ID </summary>
        [DataMember]
        public string ContentItemId { get; set; }

        /// <summary> Linked document ID </summary>
        [DataMember]
        public int DocumentId { get; set; }

        /// <summary> Is it latest version of content item or not </summary>
        [DataMember]
        public bool Latest { get; set; }

        /// <summary> Is content item published or not </summary>
        [DataMember]
        public bool Published { get; set; }

        /// <summary> Technical name of an content type in CMS </summary>
        [DataMember]
        public string ContentType { get; set; }

        /// <summary> Content </summary>
        [DataMember]
        public T Content { get; set; }
    }
}
using LinqToDB.Mapping;

namespace OrchardCoreModule.WebApi.Repository.DbClasses
{
	internal class DbContentItemIndex
	{
		[Column("Id")]
		public int Id { get; set; }

		[Column("ContentItemId")]
		public string ContentItemId { get; set; }

		[Column("Latest")]
		public bool Latest { get; set; }

		[Column("Published")]
		public bool Published { get; set; }

		[Column("ContentType")]
		public string ContentType { get; set; }

		[Column("DocumentId")]
		public int DocumentId { get; set; }
		
		[Column("Content")]
		public string Content { get; set; }
		
		[Column("IsDeleted")]
		public bool IsDeleted { get; set; }
	}
}
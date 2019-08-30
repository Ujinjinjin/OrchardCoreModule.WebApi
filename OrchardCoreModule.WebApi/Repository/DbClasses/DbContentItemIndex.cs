namespace OrchardCoreModule.WebApi.Repository.DbClasses
{
	internal class DbContentItemIndex
	{
		public int Id { get; set; }

		public string ContentItemId { get; set; }

		public int DocumentId { get; set; }

		public bool Latest { get; set; }

		public bool Published { get; set; }

		public string ContentType { get; set; }
		
		public string Content { get; set; }
	}
}
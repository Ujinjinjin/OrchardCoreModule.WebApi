create or alter procedure cms__get_content_list(
	@p_content_type varchar(255),
	@p_published bit
) as begin
	set nocount on;
	--------------------------------
    select
		ContentItemIndex.Id,
		ContentItemIndex.ContentItemId,
		ContentItemIndex.Latest,
		ContentItemIndex.Published,
		ContentItemIndex.ContentType,
		Document.Id as DocumentId,
		Document.Content
	from ContentItemIndex
	join Document
		on ContentItemIndex.DocumentId = Document.Id
	where 1 = 1
		and ContentItemIndex.ContentType = @p_content_type
		and (@p_published is null or ContentItemIndex.Published = @p_published)
	;
end
go;
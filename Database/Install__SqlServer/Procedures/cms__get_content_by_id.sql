create or alter procedure cms__get_content_by_id(
	@p_content_item_id varchar(26)
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
		and ContentItemIndex.ContentItemId = @p_content_item_id
	;
end
go;
create or alter procedure cms__get_content_list(
	@p_content_type varchar(255),
	@p_date_from datetime,
	@p_published bit,
	@p_is_deleted bit
) as begin
	set nocount on;
	--------------------------------
	select distinct
		inner_table.Id,
		outer_table.ContentItemId,
		inner_table.Latest,
		inner_table.Published,
		inner_table.ContentType,
		inner_table.ModifiedUtc,
		inner_table.DocumentId as DocumentId,
		inner_table.Content,
		inner_table.IsDeleted
	from ContentItemIndex as outer_table
	cross apply (
		select top 1
			ContentItemIndex.Id,
			ContentItemIndex.ContentItemId,
			ContentItemIndex.Latest,
			ContentItemIndex.Published,
			ContentItemIndex.ContentType,
			ContentItemIndex.ModifiedUtc,
			Document.Id as DocumentId,
			Document.Content,
			iif(ContentItemIndex.Latest = 0 and ContentItemIndex.Published = 0, 1, 0) as IsDeleted
		from ContentItemIndex
		inner join Document
			on ContentItemIndex.DocumentId = Document.Id
		where 1 = 1
			and outer_table.ContentItemId = ContentItemIndex.ContentItemId
			and (1 = 0
				or @p_content_type is null
				or ContentItemIndex.ContentType = @p_content_type
			)
			and (1 = 0
				or @p_date_from is null
				or ContentItemIndex.ModifiedUtc >= @p_date_from
			)
			and (1 = 0
				or @p_published is null
				or ContentItemIndex.Published = @p_published
			)
			and (1 = 0
				or @p_is_deleted is null
				or iif(ContentItemIndex.Latest = 0 and ContentItemIndex.Published = 0, 1, 0) = @p_is_deleted
			)
		order by ContentItemIndex.ModifiedUtc desc
	) as inner_table
;
end
go;
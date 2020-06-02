create or replace function cms__get_content_by_id(
	p_content_type varchar(255),
	p_content_item_id varchar(26),
	p_published boolean
)
returns table (
	"Id" integer,
	"ContentItemId" varchar(26),
	"Latest" boolean,
	"Published" boolean,
	"ContentType" varchar(255),
	"DocumentId" integer,
	"Content" text,
	"IsDeleted" boolean
) as
$$
begin
	return query select
		"ContentItemIndex"."Id",
		"ContentItemIndex"."ContentItemId",
		"ContentItemIndex"."Latest",
		"ContentItemIndex"."Published",
		"ContentItemIndex"."ContentType",
		"Document"."Id" as "DocumentId",
		"Document"."Content",
		("ContentItemIndex"."Latest" = false and "ContentItemIndex"."Published" = false) as "IsDeleted"
	from "ContentItemIndex"
	join "Document"
		on "ContentItemIndex"."DocumentId" = "Document"."Id"
	where 1 = 1
		and "ContentItemIndex"."ContentItemId" = p_content_item_id
		and "ContentItemIndex"."ContentType" = p_content_type
		and (1 = 0
			or p_published is null
			or "ContentItemIndex"."Published" = p_published
		)
	order by "ContentItemIndex"."ModifiedUtc" desc
	limit 1
	;
end
$$
language plpgsql;
create or replace function cms__get_content_list(
	p_content_type varchar(255),
	p_date_from timestamp,
	p_published boolean,
	p_is_deleted boolean
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
	return query select distinct on ("outer_table"."ContentItemId")
		"inner_table"."Id",
		"outer_table"."ContentItemId",
		"inner_table"."Latest",
		"inner_table"."Published",
		"inner_table"."ContentType",
		"inner_table"."DocumentId" as DocumentId,
		"inner_table"."Content",
		"inner_table"."IsDeleted"
	from "ContentItemIndex" as outer_table
	cross join lateral (
		select
			"ContentItemIndex"."Id",
			"ContentItemIndex"."ContentItemId",
			"ContentItemIndex"."Latest",
			"ContentItemIndex"."Published",
			"ContentItemIndex"."ContentType",
			"Document"."Id" as "DocumentId",
			"Document"."Content",
			("ContentItemIndex"."Latest" = false and "ContentItemIndex"."Published" = false) as "IsDeleted"
		from "ContentItemIndex"
		inner join "Document"
			on "ContentItemIndex"."DocumentId" = "Document"."Id"
		where 1 = 1
			and "outer_table"."ContentItemId" = "ContentItemIndex"."ContentItemId"
			and (1 = 0
				or p_content_type is null
				or "ContentItemIndex"."ContentType" = p_content_type
			)
			and (1 = 0
				or p_date_from is null
				or "ContentItemIndex"."ModifiedUtc" > cast(p_date_from as timestamp)
			)
			and (1 = 0
				or p_published is null
				or "ContentItemIndex"."Published" = p_published
			)
			and (1 = 0
				or p_is_deleted is null
				or ("ContentItemIndex"."Latest" = false and "ContentItemIndex"."Published" = false) = p_is_deleted
			)
		order by "ContentItemIndex"."ModifiedUtc" desc
		limit 1
	) as inner_table
	;
end
$$
language plpgsql;

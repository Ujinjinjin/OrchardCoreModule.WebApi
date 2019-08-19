using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Queries;
using OrchardCore.Queries.Sql;
using OrchardCoreModule.WebApi.Abstractions;

namespace OrchardCoreModule.WebApi.Controllers
{
    [AllowAnonymous]
    public class ContentController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly IQueryManager _queryManager;

        public ContentController(IContentManager contentManager, IQueryManager queryManager)
        {
            _contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
            _queryManager = queryManager ?? throw new ArgumentNullException(nameof(queryManager));
        }

        [Route("api/content/get")]
        public async Task<ContentItem> GetContentItem(GetContentItemRequest request)
        {
            return await _contentManager.GetAsync(request.Id);
        }
        
        [Route("api/content/list")]
        public async Task<ContentItemIndex[]> GetContentItemList(GetContentItemListRequest request)
        {
            var queryTemplate = "select" +
                                "    Orchard_ContentItemIndex.Id," +
                                "    Orchard_ContentItemIndex.ContentItemId," +
                                "    Orchard_ContentItemIndex.Latest," +
                                "    Orchard_ContentItemIndex.Published," +
                                "    Orchard_ContentItemIndex.ContentType," +
                                "    Orchard_Document.Id," +
                                "    Orchard_Document.Content" +
                                "from Orchard_ContentItemIndex" +
                                "join Orchard_Document" +
                                "    on Orchard_ContentItemIndex.DocumentId = Orchard_Document.Id" +
                                $"where Orchard_ContentItemIndex.ContentType = N'{request.ContentType}'";
        
            var queryResult = await _queryManager.ExecuteQueryAsync(new SqlQuery
            {
                Name = "GetObjectList",
                Template = "SELECT * FROM [ContentItemIndex];"
            }, new Dictionary<string, object>());

            var contentItems = ((List<JObject>) queryResult).Select(item => item.ToObject<ContentItemIndex>());
            
            return contentItems.Where(item =>
                    string.IsNullOrEmpty(request.ContentType) || 
                    item.ContentType == request.ContentType)
                .ToArray();
        }
    }
}
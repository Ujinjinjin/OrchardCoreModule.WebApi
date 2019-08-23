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
        public async Task<object> GetContentItem(GetContentItemRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return BadRequest($"Invalid value {nameof(request.Id)}");
            }
            return await _contentManager.GetAsync(request.Id);
        }
        
        [Route("api/content/list")]
        public async Task<object> GetContentItemList(GetContentItemListRequest request)
        {
            try
            {
                var queryTemplate = "select" +
                                    "    ContentItemIndex.Id," +
                                    "    ContentItemIndex.ContentItemId," +
                                    "    ContentItemIndex.Latest," +
                                    "    ContentItemIndex.Published," +
                                    "    ContentItemIndex.ContentType," +
                                    "    Document.Id as DocumentId," +
                                    "    Document.Content" +
                                    "from ContentItemIndex" +
                                    "join Document" +
                                    "    on ContentItemIndex.DocumentId = Document.Id" +
                                    $"where ContentItemIndex.ContentType = '{request.ContentType}'";
        
                var queryResult = await _queryManager.ExecuteQueryAsync(new SqlQuery
                {
                    Name = "GetObjectList",
                    Template = queryTemplate
                }, new Dictionary<string, object>());

                if (((object[]) queryResult).Length == 0)
                {
                    return BadRequest();
                }

                var contentItems = ((List<JObject>) queryResult).Select(item => item.ToObject<ContentItemIndex>());
            
                return contentItems.Where(item =>
                        string.IsNullOrEmpty(request.ContentType) || 
                        item.ContentType == request.ContentType)
                    .ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
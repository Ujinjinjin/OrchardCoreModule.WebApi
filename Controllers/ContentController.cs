using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrchardCore.ContentManagement;
using OrchardCore.Queries;

namespace OrchardCore.WebApi.Controllers
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
        public async Task<ContentItem> GetContent()
        {
            return await _contentManager.GetAsync(null);
        }
        
        [Route("api/content/list")]
        public IActionResult GetContentList()
        {
//            _queryManager.ExecuteQueryAsync();
            return Ok();
        }
    }
}
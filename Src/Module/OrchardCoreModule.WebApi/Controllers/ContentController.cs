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
using OrchardCoreModule.WebApi.Repository;

namespace OrchardCoreModule.WebApi.Controllers
{
	internal class ContentController : Controller
	{
		private readonly IContentManager _contentManager;
		private readonly ICmsRepository _repository;

		public ContentController(IContentManager contentManager, ICmsRepository repository)
		{
			_contentManager = contentManager ?? throw new ArgumentNullException(nameof(contentManager));
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
		public IActionResult GetContentItemList(GetContentItemListRequest request)
		{
			return Ok(_repository.GetStuff());
		}
	}
}
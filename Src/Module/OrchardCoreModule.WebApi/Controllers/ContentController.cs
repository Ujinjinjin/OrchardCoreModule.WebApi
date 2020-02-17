using Microsoft.AspNetCore.Mvc;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Repository;
using System;

namespace OrchardCoreModule.WebApi.Controllers
{
	public class ContentController : Controller
	{
		private readonly ICmsRepository _repository;

		public ContentController(ICmsRepository repository)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		[Route("api/content/get")]
		public object GetContentItemById(GetContentItemRequest request)
		{
			try
			{
				_ = request.Id ?? throw new ArgumentNullException(nameof(request.Id));
				_ = request.ContentType ?? throw new ArgumentNullException(nameof(request.ContentType));

				var contentItem = _repository.GetContentItemById(request.ContentType, request.Id);

				if (contentItem == null)
				{
					return NotFound();
				}

				return contentItem;
			}
			catch (ArgumentNullException e)
			{
				return BadRequest($"missing required parameter: {e.ParamName}");
			}
		}

		[Route("api/content/getlist")]
		public object GetContentItemList(GetContentItemRequest request)
		{
			try
			{
				_ = request.ContentType ?? throw new ArgumentNullException(nameof(request.ContentType));

				var contentItemList = _repository.GetContentItemList(request.ContentType, true);

				if (contentItemList == null || contentItemList.Count == 0)
				{
					return NotFound();
				}

				return contentItemList;
			}
			catch (ArgumentNullException e)
			{
				return BadRequest($"missing required parameter: {e.ParamName}");
			}
		}
	}
}
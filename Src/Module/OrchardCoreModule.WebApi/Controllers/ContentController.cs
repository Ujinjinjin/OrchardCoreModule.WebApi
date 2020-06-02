using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Repository;
using System;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Controllers
{
	public class ContentController : Controller
	{
		private readonly ICmsRepository _repository;
		private readonly ILogger _logger;

		public ContentController(ICmsRepository repository, ILogger logger)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[Route("api/content/get")]
		public async Task<object> GetContentItemByIdAsync(GetContentItemRequest request)
		{
			try
			{
				_ = request.Id ?? throw new ArgumentNullException(nameof(request.Id));
				_ = request.ContentType ?? throw new ArgumentNullException(nameof(request.ContentType));

				var contentItem = await _repository.GetContentItemByIdAsync<object>(request.ContentType, request.Id, request.Published);

				if (contentItem == null)
				{
					return NotFound();
				}

				return contentItem;
			}
			catch (ArgumentNullException exception)
			{
				_logger.LogError($"ContentController:Error; {exception.Message}");
				return BadRequest($"missing required parameter: {exception.ParamName}");
			}
		}

		[Route("api/content/getlist")]
		public async Task<object> GetContentItemListAsync(GetContentItemListRequest request)
		{
			try
			{
				_ = request.ContentType ?? throw new ArgumentNullException(nameof(request.ContentType));

				var contentItemList = await _repository.GetContentItemListAsync<object>(request.ContentType, request.DateFrom, request.Published, request.IsDeleted);

				if (contentItemList == null || contentItemList.Count == 0)
				{
					return NotFound();
				}

				return contentItemList;
			}
			catch (ArgumentNullException exception)
			{
				_logger.LogError($"ContentController:Error; {exception.Message}");
				return BadRequest($"missing required parameter: {exception.ParamName}");
			}
		}
	}
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Const;
using OrchardCoreModule.WebApi.Converters;
using OrchardCoreModule.WebApi.Models;
using OrchardCoreModule.WebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OrchardCoreModule.WebApi.Controllers
{
	public class SynchronizationController : Controller
	{
		private readonly ICmsRepository _repository;
		private readonly ILogger _logger;

		public SynchronizationController(ICmsRepository repository, ILogger logger)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[Route("api/sync/getcallbacklist")]
		public async Task<ActionResult<IList<CallbackData>>> GetCallbackListAsync(GetCallbackListRequest request)
		{
			try
			{
				_ = request.DateFrom ?? throw new ArgumentNullException(nameof(request.DateFrom));

				var contentItemList = await _repository.GetContentItemListAsync<CallbackInfo>(KnownContentType.Callback, request.DateFrom, request.Published, false);

				if (contentItemList == null || contentItemList.Count == 0)
				{
					return Array.Empty<CallbackData>();
				}

				var contentDiff = contentItemList
					.GroupBy(x => x.Content.Callback.ContentItemId.Text)
					.Select(
						x =>
							x.OrderBy(y => y.Content.ModifiedUtc)
								.LastOrDefault()
								?.Content.ToInterface()
					)
					.Where(x => x != null)
					.ToArray();

				return contentDiff;
			}
			catch (ArgumentNullException exception)
			{
				_logger.LogError($"ContentController:Error; {exception.Message}");
				return BadRequest($"missing required parameter: {exception.ParamName}");
			}
		}
	}
}
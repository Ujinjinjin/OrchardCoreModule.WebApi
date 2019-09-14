using System;
using Microsoft.AspNetCore.Mvc;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Repository;

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
		public object GetContentItem(GetContentItemRequest request)
		{
			_ = request.Id ?? throw new ArgumentNullException(nameof(request.Id));

			return _repository.GetContentItemById(request.Id);
		}

		[Route("api/content/list")]
		public object GetContentItemList(GetContentItemListRequest request)
		{
			_ = request.ContentType ?? throw new ArgumentNullException(nameof(request.ContentType));

			return _repository.GetContentItemList(request.ContentType, null);
		}
	}
}
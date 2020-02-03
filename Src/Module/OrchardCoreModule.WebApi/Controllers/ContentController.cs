using Microsoft.AspNetCore.Mvc;
using OrchardCoreModule.WebApi.Abstractions;
using OrchardCoreModule.WebApi.Const;
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

		[Route("api/content/getstorybyid")]
		public object GetStoryById(GetContentItemRequest request)
		{
			_ = request.Id ?? throw new ArgumentNullException(nameof(request.Id));

			var story = _repository.GetContentItemById(ContentType.Story, request.Id);

			if (story == null)
			{
				return NotFound();
			}

			return story;
		}

		[Route("api/content/getstorylist")]
		public object GetStoryList()
		{
			return _repository.GetContentItemList(ContentType.Story, true);
		}

		[Route("api/content/getfabyid")]
		public object GetFaById(GetContentItemRequest request)
		{
			_ = request.Id ?? throw new ArgumentNullException(nameof(request.Id));

			var faq = _repository.GetContentItemById(ContentType.FaqQuestion, request.Id);

			if (faq == null)
			{
				return NotFound();
			}

			return faq;
		}

		[Route("api/content/getfaqlist")]
		public object GetFaqList()
		{
			return _repository.GetContentItemList(ContentType.FaqQuestion, true);
		}

		[Route("api/content/getfaqsectionlist")]
		public object GetFaqSectionList()
		{
			return _repository.GetContentItemList(ContentType.FaqSection, true);
		}
	}
}
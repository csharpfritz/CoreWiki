using CoreWiki.Application;
using CoreWiki.Application.Articles.Commands;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Areas.Identity;
using CoreWiki.Data.Data.Interfaces;

using CoreWiki.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NodaTime;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{

	[Authorize(Policy = PolicyConstants.CanWriteArticles)]
	public class CreateModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IArticleRepository _articleRepo;
		public ILogger Logger { get; private set; }

		public CreateModel(IMediator mediator, IArticleRepository articleRepo, ILoggerFactory loggerFactory)
		{
			_mediator = mediator;
			_articleRepo = articleRepo;
			this.Logger = loggerFactory.CreateLogger("CreatePage");
		}

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (string.IsNullOrEmpty(slug))
			{
				var _returnModel = await _mediator.Send(new CreateNewArticleQuery(UrlHelpers.SlugToTopic(slug)));
				Article = _returnModel.NewArticle;
				return Page();
			}
			if (await _articleRepo.GetArticleBySlug(slug) != null)
			{
				return Redirect($"/{slug}/Edit");
			}

			return Page();
		}

		[BindProperty]
		public ArticleCreateDTO Article { get; set;}   //CoreWiki.Application DTO

		public async Task<IActionResult> OnPostAsync()
		{
			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (string.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			if (!ModelState.IsValid) { return Page(); }

			Logger.LogWarning($"Creating page with slug: {slug}");

			if (await _articleRepo.IsTopicAvailable(slug, 0))
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
				return Page();
			}
			
			Article.AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			Article.AuthorName = User.Identity.Name;
			Article.Slug = slug;

			var _articleLinks = await _mediator.Send(new CreateNewArticleCommand(Article));

			if (_articleLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{slug}");
		}
	}
}

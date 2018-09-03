using CoreWiki.Areas.Identity;
using CoreWiki.ViewModels;
using CoreWiki.Application.Articles.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Globalization;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Helpers;

namespace CoreWiki.Pages
{

	[Authorize(Policy = PolicyConstants.CanWriteArticles)]
	public class CreateModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly ILogger _logger;

		public CreateModel(IMediator mediator, ILoggerFactory loggerFactory)
		{
			_mediator = mediator;
			_logger = loggerFactory.CreateLogger("CreatePage");
		}

		public async Task<IActionResult> OnGetAsync(string slug = "")
		{
			if (string.IsNullOrEmpty(slug))
			{
				return Page();
			}

			var request = new GetArticleQuery(slug);
			var result = await _mediator.Send(request);
			if (result == null)
			{
				Article = new ArticleCreate
				{
					Topic = UrlHelpers.SlugToTopic(slug)
				};
			}
			else
			{
				return Redirect($"/{slug}/Edit");
			}

			return Page();
		}

		[BindProperty]
		public ArticleCreate Article { get; set; }   

		public async Task<IActionResult> OnPostAsync()
		{
			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (string.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			if (!ModelState.IsValid) { return Page(); }

			_logger.LogWarning($"Creating page with slug: {slug}");

			var isTopicAvailable = new GetIsTopicAvailableQuery {Slug = slug, ArticleId = 0};
			if (await _mediator.Send(isTopicAvailable))
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
				return Page();
			}

			var cmd = new CreateNewArticleCommand(
				authorId: Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
				authorName: User.Identity.Name,
				content: Article.Content,
				slug: slug,
				topic: Article.Topic
			);

			var cmdResult = await _mediator.Send(cmd);

			// TODO: Inspect result to ensure it ran properly

			var query = new GetArticlesToCreateFromArticle(slug);
			var listOfSlugs = await _mediator.Send(query);

			if (listOfSlugs.Any())
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{slug}");

		}
	}
}

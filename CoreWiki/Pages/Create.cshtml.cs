using CoreWiki.Areas.Identity;
using CoreWiki.Core.Interfaces;
using CoreWiki.ViewModels;
using CoreWiki.Application;
using CoreWiki.Application.Articles.Commands;
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
using CoreWiki.Core.Domain;
using System.Globalization;
using CoreWiki.Application.Articles.Queries;
using CoreWiki.Application.Helpers;

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

		public async Task<IActionResult> OnGetAsync(string slug = "")
		{

			if (!string.IsNullOrEmpty(slug))
			{

				var request = new GetArticle(slug);
				var result = await _mediator.Send(request);
				if (result == null)
				{
					Article = new ArticleCreate
					{
						Topic = SlugToTopic(slug)
					};
				}
				else
				{
					return Redirect($"/{slug}/Edit");
				}

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

			Logger.LogWarning($"Creating page with slug: {slug}");

			if (await _articleRepo.IsTopicAvailable(slug, 0))
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

		public static string SlugToTopic(string slug)
		{
			if (string.IsNullOrEmpty(slug))
			{
				return "";
			}

			var textInfo = new CultureInfo("en-US", false).TextInfo;
			var outValue = textInfo.ToTitleCase(slug);

			return outValue.Replace("-", " ");

		}
	}
}

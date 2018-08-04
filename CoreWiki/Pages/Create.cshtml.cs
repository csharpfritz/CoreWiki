using CoreWiki.Areas.Identity;
using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Helpers;
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

	[Authorize(Policy =PolicyConstants.CanWriteArticles)]
	public class CreateModel : PageModel
	{

		private readonly IArticleRepository _articleRepo;
		private readonly IClock _clock;

		public ILogger Logger { get; private set; }

		public CreateModel(IArticleRepository articleRepo, IClock clock, ILoggerFactory loggerFactory)
		{
			_articleRepo = articleRepo;
			_clock = clock;
			this.Logger = loggerFactory.CreateLogger("CreatePage");
		}

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (string.IsNullOrEmpty(slug))
			{
				return Page();
			}

			if (await _articleRepo.GetArticleBySlug(slug) != null)
			{
				return Redirect($"/{slug}/Edit");
			}

			Article = new ArticleCreateDTO()
			{
				Topic = UrlHelpers.SlugToTopic(slug)
			};

			return Page();
		}

		[BindProperty]
		public ArticleCreateDTO Article { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{

			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (string.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			var article = new Article();
			article.Topic = Article.Topic;
			article.Slug = slug;
			article.AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			article.AuthorName = User.Identity.Name;

			if (!ModelState.IsValid)
			{
				return Page();
			}

			//check if the slug already exists in the database.
			Logger.LogWarning($"Creating page with slug: {slug}");

			if (await _articleRepo.IsTopicAvailable(slug, 0))
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
				return Page();
			}

			article.Published = _clock.GetCurrentInstant();

			article = await _articleRepo.CreateArticleAndHistory(article);


			var articlesToCreateFromLinks = (await ArticleHelpers.GetArticlesToCreate(_articleRepo, article, createSlug: true))
				.ToList();

			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{article.Slug}");
		}
	}
}

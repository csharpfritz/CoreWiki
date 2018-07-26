using CoreWiki.Data;
using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Extensibility.Common;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{

	public class EditModel : PageModel
	{

		private readonly IArticleRepository _Repo;
		private readonly ISlugHistoryRepository _SlugRepo;
		private readonly IClock _clock;

		private readonly CoreWikiModuleEvents _moduleEvents; // MAC - this can be obtained from an injected service to make it mockable (testable)
		private readonly IExtensibilityManager _extensibilityManager; // MAC

		public EditModel(IArticleRepository articleRepo, ISlugHistoryRepository slugHistoryRepository, IClock clock,
						 IExtensibilityManager extensibilityManager)
		{
			_Repo = articleRepo;
			_SlugRepo = slugHistoryRepository;
			_clock = clock;

			_extensibilityManager = extensibilityManager; // MAC
			_moduleEvents = Startup.ModuleEvents; // MAC
		}

		[BindProperty]
		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			Article = await _Repo.GetArticleBySlug(slug);

			if (Article == null)
			{
				return new ArticleNotFoundResult();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var existingArticle = await _Repo.GetArticleById(Article.Id);
			Article.ViewCount = existingArticle.ViewCount;
			Article.Version = existingArticle.Version + 1;

			// MAC - check PreSubmitArticle extensibility event
			if (_moduleEvents.PreSubmitArticle != null)
			{
				var args = new PreSubmitArticleEventArgs(Article.Topic, Article.Content);

				_extensibilityManager.InvokeCancelableModuleEvent(_moduleEvents.PreSubmitArticle, args);

				if (args.Cancel)
				{
					if (!string.IsNullOrWhiteSpace(args.ModelErrorProperty))
						ModelState.AddModelError("Article" + args.ModelErrorProperty, args.ModelErrorMessage);

					return Page();
				}

				Article.Topic = args.Topic;
				Article.Content = args.Content;
			}
			// MAC

			//check if the slug already exists in the database.
			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (String.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			var articlesToCreateFromLinks = (await ArticleHelpers.GetArticlesToCreate(_Repo, Article, createSlug: true))
				.ToList();

			Article.Published = _clock.GetCurrentInstant();
			Article.Slug = slug;
			Article.AuthorId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
			Article.AuthorName = User.Identity.Name;

			if (!string.Equals(Article.Slug, existingArticle.Slug, StringComparison.InvariantCulture))
			{
				await _SlugRepo.AddToHistory(existingArticle.Slug, Article);
			}

			//AddNewArticleVersion();

			// MAC - check ArticleSubmitted extensibility event
			if (_moduleEvents.ArticleSubmitted != null)
			{
				var args = new ArticleSubmittedEventArgs(Article.Topic, Article.Content);
				_extensibilityManager.InvokeModuleEvent(_moduleEvents.ArticleSubmitted, args);
			}
			// MAC

			try
			{
				await _Repo.Update(Article);
			}
			catch (ArticleNotFoundException)
			{
				return new ArticleNotFoundResult();
			}

			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{(Article.Slug == UrlHelpers.HomePageSlug ? "" : Article.Slug)}");
		}


	}

}

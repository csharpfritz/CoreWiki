using CoreWiki.Data;
using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Data.Repositories;
using CoreWiki.Data.Models;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

		public EditModel(IArticleRepository articleRepo, ISlugHistoryRepository slugHistoryRepository, IClock clock)
		{
			_Repo = articleRepo;
			_SlugRepo = slugHistoryRepository;
			_clock = clock;
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

			//check if the slug already exists in the database.
			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (String.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The Topic must contain at least one alphanumeric character.");
				return Page();
			}

			if (!await _Repo.IsTopicAvailable(slug, Article.Id))
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
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

			try {
				await _Repo.Update(Article);
			} catch (ArticleNotFoundException) {
				return new ArticleNotFoundResult();
			}

			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/{(Article.Slug == "home-page" ? "" : Article.Slug)}");
		}

	
	}

}

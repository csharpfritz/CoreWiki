using CoreWiki.Data;
using CoreWiki.Core.Interfaces;
using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreWiki.Data.EntityFramework;

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
		public ArticleEdit Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var article = await _Repo.GetArticleBySlug(slug);

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			Article = new ArticleEdit
			{
				Id = article.Id,
				Topic = article.Topic,
				Content = article.Content
			};

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var slug = UrlHelpers.URLFriendly(Article.Topic);
			if (String.IsNullOrWhiteSpace(slug))
			{
				ModelState.AddModelError("Article.Topic", "The topic must contain at least one alphanumeric character.");
				return Page();
			}

			var existingArticle = await _Repo.GetArticleBySlug(slug);
			if (existingArticle != null && existingArticle.Id != Article.Id)
			{
				ModelState.AddModelError("Article.Topic", "The topic conflicts with an existing article.");
				return Page();
			}

			if (existingArticle == null)
			{
				existingArticle = await _Repo.GetArticleById(Article.Id);
			}

			if (!Changed(existingArticle.Topic, Article.Topic) && !Changed(existingArticle.Content, Article.Content))
			{
				return Redirect($"/wiki/{existingArticle.Slug}");
			}

			var oldSlug = existingArticle.Slug;

			existingArticle.Topic = Article.Topic;
			existingArticle.Slug = slug;
			existingArticle.Content = Article.Content;
			existingArticle.Version = existingArticle.Version + 1;
			existingArticle.Published = _clock.GetCurrentInstant();
			existingArticle.AuthorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			existingArticle.AuthorName = User.Identity.Name;

			try
			{
				await _Repo.Update(existingArticle);

				if (Changed(oldSlug, existingArticle.Slug))
				{
					await _SlugRepo.AddToHistory(oldSlug, existingArticle);
				}
			}
			catch (ArticleNotFoundException)
			{
				return new ArticleNotFoundResult();
			}

			var articlesToCreateFromLinks = (await ArticleHelpers.GetArticlesToCreate(_Repo, existingArticle, createSlug: true)).ToList();
			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/wiki/{(existingArticle.Slug == UrlHelpers.HomePageSlug ? "" : existingArticle.Slug)}");
		}

		private bool Changed(string v1, string v2)
			=> !string.Equals(v1, v2, StringComparison.InvariantCulture);
	}

}

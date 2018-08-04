using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NodaTime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	[Authorize]
	public class CreateArticleFromLinkModel : PageModel
	{
		[BindProperty]
		public ArticleCreateFromLinkDTO Article { get; set; }
		[BindProperty]
		public List<string> LinksToCreate { get; set; } = new List<string>();

		private readonly IArticleRepository _articleRepo;
		private readonly IClock _clock;

		public CreateArticleFromLinkModel(IArticleRepository articleRepo, IClock clock)
		{
			_articleRepo = articleRepo;
			_clock = clock;
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _articleRepo.GetArticleBySlug(id);

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			LinksToCreate = (await ArticleHelpers.GetArticlesToCreate(_articleRepo, article)).ToList();

			if (LinksToCreate.Count == 0)
			{
				return Redirect($"/wiki/{(article.Slug == UrlHelpers.HomePageSlug ? "" : article.Slug)}");
			}

			Article = new ArticleCreateFromLinkDTO
			{
				Slug = article.Slug
			};

			return Page();
		}

		public async Task<IActionResult> OnPostCreateLinksAsync(string slug)
		{
			foreach (var link in LinksToCreate)
			{
				var newArticle = new Article
				{
					Slug = link,
					Topic = link.ToTitleCase()
								.RemoveHyphens(),
					Published = _clock.GetCurrentInstant(),
					Content = string.Empty
				};

				newArticle = await _articleRepo.CreateArticleAndHistory(newArticle);

			}

			return Redirect($"/wiki/{(slug == UrlHelpers.HomePageSlug ? "" : slug)}");
		}

		public IActionResult OnPostCancel(string slug)
		{
			return Redirect($"/wiki/{(slug == UrlHelpers.HomePageSlug ? "" : slug)}");
		}
	}
}

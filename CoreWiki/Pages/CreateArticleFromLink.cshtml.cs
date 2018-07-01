using CoreWiki.Helpers;
using CoreWiki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
		public Article Article { get; set; }
		[BindProperty]
		public List<string> LinksToCreate { get; set; } = new List<string>();

		private readonly IApplicationDbContext _context;
		private readonly IClock _clock;

		public CreateArticleFromLinkModel(IApplicationDbContext context, IClock clock)
		{
			_context = context;
			_clock = clock;
		}

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == id);

			if (Article == null)
			{
				return new ArticleNotFoundResult();
			}

			LinksToCreate = ArticleHelpers.GetArticlesToCreate(_context, Article).ToList();

			if (LinksToCreate.Count == 0)
			{
				return Redirect($"/{(Article.Slug == "home-page" ? "" : Article.Slug)}");
			}

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

				_context.Articles.Add(newArticle);
				await _context.SaveChangesAsync();
			}

			return Redirect($"/{(slug == "home-page" ? "" : slug)}");
		}

		public IActionResult OnPostCancel(string slug)
		{
			return Redirect($"/{(slug == "home-page" ? "" : slug)}");
		}
	}
}

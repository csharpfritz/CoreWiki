using CoreWiki.Data;
using CoreWiki.Models;
using CoreWiki.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	[Authorize("CanDeleteArticles")]

	public class DeleteModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public DeleteModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public ArticleDeleteDTO Article { get; set; }

		///  TODO: Make it so you cannot delete the home page (deleting the home page will cause a 404)
		///  or re-factor to make the home page dynamic or configurable.
		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			Article = new ArticleDeleteDTO
			{
				Content = article.Content,
				Published = article.Published,
				Topic = article.Topic
			};

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			var article = await _context.Articles
				.Include(o => o.History)
				.Include(o => o.Comments)
				.SingleOrDefaultAsync(o => o.Slug == slug);

			if (article != null)
			{
				_context.Articles.Remove(article);
				await _context.SaveChangesAsync();
			}

			return LocalRedirect($"/wiki/{UrlHelpers.HomePageSlug}");
		}
	}
}

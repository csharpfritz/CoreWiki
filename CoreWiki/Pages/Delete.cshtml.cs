using CoreWiki.Models;
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
		private readonly IApplicationDbContext _context;

		public DeleteModel(IApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Article Article { get; set; }

		///  TODO: Make it so you cannot delete the home page (deleting the home page will cause a 404)
		///  or re-factor to make the home page dynamic or configurable.
		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

			if (Article == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

			if (Article != null)
			{
				_context.Articles.Remove(Article);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("/All");
		}
	}
}

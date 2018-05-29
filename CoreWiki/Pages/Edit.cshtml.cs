using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;
using NodaTime;
using CoreWiki.Helpers;

namespace CoreWiki.Pages
{

	public class EditModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;
		private readonly IClock _clock;

		public EditModel(CoreWiki.Models.ApplicationDbContext context, IClock clock)
		{
			_context = context;
			_clock = clock;
		}

		[BindProperty]
		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return new ArticleNotFoundResult();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == id);

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

			var existingArticle = _context.Articles.AsNoTracking().First(a => a.Topic == Article.Topic);
			Article.ViewCount = existingArticle.ViewCount;

			_context.Attach(Article).State = EntityState.Modified;
			Article.Published = _clock.GetCurrentInstant();

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ArticleExists(Article.Topic))
				{
					return new ArticleNotFoundResult();
				}
				else
				{
					throw;
				}
			}

			return Redirect($"/{(Article.Topic == "HomePage" ? "" : Article.Topic)}");
		}

		private bool ArticleExists(string id)
		{
			return _context.Articles.Any(e => e.Topic == id);
		}
	}

}

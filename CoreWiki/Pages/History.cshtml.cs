using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Helpers;
using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Pages
{
	public class HistoryModel : PageModel
	{

		private ApplicationDbContext _context { get; }

		public HistoryModel(ApplicationDbContext context)
		{

			this._context = context;

		}

		public Article Article { get; private set; }

		public async Task<IActionResult> OnGet(string slug)
		{

			if (string.IsNullOrEmpty(slug))
			{
				return NotFound();
			}

			Article = await _context.Articles
				.Include(a => a.History)
				.SingleOrDefaultAsync(m => m.Slug == slug);

			if (Article == null)
			{
				return new ArticleNotFoundResult();
			}

			return Page();

		}
	}
}

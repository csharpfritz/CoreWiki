using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Helpers;
using CoreWiki.Models;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
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

		[BindProperty()]
		public string[] Compare { get; set; }

		public SideBySideDiffModel DiffModel { get; set; }

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

		public async Task<IActionResult> OnPost(string slug) {

			Article = await _context.Articles
			.Include(a => a.History)
			.SingleOrDefaultAsync(m => m.Slug == slug);

			var histories = Article.History
				.Where(h => Compare.Any(c => c == h.Version.ToString()))
				.OrderBy(h => h.Version)
				.ToArray();


			this.DiffModel = new SideBySideDiffBuilder(new DiffPlex.Differ())
				.BuildDiffModel(histories[0].Content, histories[1].Content);

			return Page();

		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;

namespace CoreWiki.Pages
{
	public class EditModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;

		public EditModel(CoreWiki.Models.ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == id);

			if (Article == null)
			{
				return NotFound();
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			_context.Attach(Article).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ArticleExists(Article.Topic))
				{
					return NotFound();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;

namespace CoreWiki.Pages
{
	public class DeleteModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;

		public DeleteModel(CoreWiki.Models.ApplicationDbContext context)
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

		public async Task<IActionResult> OnPostAsync(string id)
		{
			if (id == null)
			{
				return NotFound();
			}
			else if (id == "HomePage")
			{
				return NotFound();
			}

			Article = await _context.Articles.FindAsync(id);

			if (Article != null)
			{
				_context.Articles.Remove(Article);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Details");
		}
	}
}

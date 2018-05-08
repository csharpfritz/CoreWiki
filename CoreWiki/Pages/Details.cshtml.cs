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
	public class DetailsModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;

		public DetailsModel(CoreWiki.Models.ApplicationDbContext context)
		{
			_context = context;
		}

		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

			// TODO: If topicName not specified, default to Home Page

			slug = slug ?? "home-page";

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug.ToLower());

			if (Article == null)
			{
				return NotFound();
			}

			Article.ReadCount++;
			await _context.SaveChangesAsync();

			return Page();
		}
	}
}

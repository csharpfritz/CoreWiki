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

		public async Task<IActionResult> OnGetAsync(string topicName)
		{

			// TODO: If topicName not specified, default to Home Page

			topicName = topicName ?? "HomePage";

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Topic == topicName);

			if (Article == null)
			{
				return NotFound();
			}

			if (Request.Cookies[Article.Topic] == null)
			{
				Article.ViewCount++;
				Response.Cookies.Append(Article.Topic, "foo", new Microsoft.AspNetCore.Http.CookieOptions
				{
					Expires = DateTime.UtcNow.AddMinutes(5)
				});

				await _context.SaveChangesAsync();
			}

			return Page();
		}
	}
}

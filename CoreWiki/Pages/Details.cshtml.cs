using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;
using NodaTime;

namespace CoreWiki.Pages
{
	public class DetailsModel : PageModel
	{
		private readonly CoreWiki.Models.ApplicationDbContext _context;
		private readonly IClock _clock;
		public DetailsModel(CoreWiki.Models.ApplicationDbContext context, IClock clock)
		{
			_context = context;
			_clock = clock;
		}

		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{

            // TODO: If topicName not specified, default to Home Page

            slug = slug ?? "home-page";

			Article = await _context.Articles.Include(x => x.Comments).SingleOrDefaultAsync(m => m.Slug == slug.ToLower());

			if (Article == null)
			{
				return NotFound();
			}
			return Page();
	}

		public async Task<IActionResult> OnPostAsync(Models.Comment comment)
		{
			TryValidateModel(comment);
			Article = await _context.Articles.Include(x => x.Comments).SingleOrDefaultAsync(m => m.Id == comment.IdArticle);

			if (Article == null)
								 return NotFound();

			if (!ModelState.IsValid)
								 return Page();

			comment.Article = this.Article;

			comment.Submitted = _clock.GetCurrentInstant();

			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();

			return Redirect($"/{Article.Slug}");
		}
	}
}

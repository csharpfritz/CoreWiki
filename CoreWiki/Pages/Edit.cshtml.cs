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

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);

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

			//check if the slug already exists in the database.  
			var slug = UrlHelpers.URLFriendly(Article.Topic.ToLower());
			var isAvailable = !_context.Articles.Any(x => x.Slug == slug && x.Id != Article.Id);

			if (isAvailable == false)
			{
				ModelState.AddModelError("Article.Topic", "This Title already exists.");
				return Page();
			}

			_context.Attach(Article).State = EntityState.Modified;

			Article.Published = _clock.GetCurrentInstant();
			Article.Slug = slug;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ArticleExists(Article.Id))
				{
					return new ArticleNotFoundResult();
				}
				else
				{
					throw;
				}
			}

			return Redirect($"/{(Article.Slug == "home-page" ? "" : Article.Slug)}");
		}

		private bool ArticleExists(int id)
		{
			return _context.Articles.Any(e => e.Id == id);
		}
	}

}

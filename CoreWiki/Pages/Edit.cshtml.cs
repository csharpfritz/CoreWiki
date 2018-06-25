﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;
using NodaTime;
using CoreWiki.Helpers;

namespace CoreWiki.Pages
{

	public class EditModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IClock _clock;

		public EditModel(ApplicationDbContext context, IClock clock)
		{
			_context = context;
			_clock = clock;
		}

		[BindProperty]
		public Article Article { get; set; }

		public async Task<IActionResult> OnGetAsync(string slug)
		{
			if (slug == null)
			{
				return NotFound();
			}

			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

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

			var articlesToCreateFromLinks = ArticleHelpers.GetArticlesToCreate(_context, Article, createSlug: true)
				.ToList();

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

			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/{(Article.Slug == "home-page" ? "" : Article.Slug)}");
		}

		private bool ArticleExists(int id)
		{
			return _context.Articles.Any(e => e.Id == id);
		}
	}

}

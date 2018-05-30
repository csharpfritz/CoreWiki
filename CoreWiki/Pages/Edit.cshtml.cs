﻿using System;
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
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Pages
{
	[Authorize]
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

		public async Task<IActionResult> OnGetAsync(int id)
		{
			Article = await _context.Articles.SingleOrDefaultAsync(m => m.Id == id);

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

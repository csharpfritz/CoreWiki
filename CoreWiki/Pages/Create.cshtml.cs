﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodaTime;
using CoreWiki.Models;
using CoreWiki.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Pages
{
	[Authorize]
	public class CreateModel : PageModel
    {
        private readonly CoreWiki.Models.ApplicationDbContext _context;
        private readonly IClock _clock;

    public ILogger Logger { get; private set; }

    public CreateModel(CoreWiki.Models.ApplicationDbContext context, IClock clock, ILoggerFactory loggerFactory)
        {
            _context = context;
            _clock = clock;
            this.Logger = loggerFactory.CreateLogger("CreatePage");
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            var slug = UrlHelpers.URLFriendly(Article.Topic.ToLower());
            Article.Slug = slug;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //check if the slug already exists in the database.  
            Logger.LogWarning($"Creating page with slug: {slug}");
            var isAvailable = !_context.Articles.Any(x => x.Slug == slug);

            if (isAvailable == false)
            {
                ModelState.AddModelError("Article.Topic", "This Title already exists.");
                return Page();
            }

            Article.Published = _clock.GetCurrentInstant();
            // Article.Slug = slug;

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return Redirect($"/{Article.Slug}");
        }
    }
}

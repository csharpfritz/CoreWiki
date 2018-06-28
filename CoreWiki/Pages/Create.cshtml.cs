using System;
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
using CoreWiki.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace CoreWiki.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IClock _clock;

    public ILogger Logger { get; private set; }

    public CreateModel(ApplicationDbContext context, IClock clock, ILoggerFactory loggerFactory)
        {
            _context = context;
            _clock = clock;
            this.Logger = loggerFactory.CreateLogger("CreatePage");
        }

        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return Page();
            }

            Article article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

            if (article != null)
            {
                return Redirect($"/{slug}/Edit");
            }

            Article = new Article()
            {
                Topic = UrlHelpers.SlugToTopic(slug)
            };

            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            var slug = UrlHelpers.URLFriendly(Article.Topic.ToLower());
            Article.Slug = slug;
						Article.AuthorId = Guid.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

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

			var articlesToCreateFromLinks = ArticleHelpers.GetArticlesToCreate(_context, Article, createSlug: true)
				.ToList();

			if (articlesToCreateFromLinks.Count > 0)
			{
				return RedirectToPage("CreateArticleFromLink", new { id = slug });
			}

			return Redirect($"/{Article.Slug}");
        }
    }
}

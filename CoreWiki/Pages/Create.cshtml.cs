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

namespace CoreWiki.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CoreWiki.Models.ApplicationDbContext _context;
        private readonly IClock _clock;

        public CreateModel(CoreWiki.Models.ApplicationDbContext context, IClock clock)
        {
            _context = context;
            _clock = clock;
        }

        public IActionResult OnGet() => Page();

        [BindProperty]
        public Article Article { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //check if the slug already exists in the database.  
            var slug = UrlHelpers.URLFriendly(Article.Topic.ToLower());
            var isAvailable = !_context.Articles.Any(x => x.Slug == slug);

            if (isAvailable == false)
            {
                ModelState.AddModelError("Article.Topic", "This Title already exists.");
                return Page();
            }

            Article.Published = _clock.GetCurrentInstant();
            Article.Slug = slug;

            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            return Redirect($"/{Article.Slug}");
        }
    }
}
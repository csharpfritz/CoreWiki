using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreWiki.Models;
using Microsoft.AspNetCore.Authorization;

namespace CoreWiki.Pages
{
		[Authorize("CanDeleteArticles")]

		public class DeleteModel : PageModel
    {
        private readonly CoreWiki.Models.ApplicationDbContext _context;

        public DeleteModel(CoreWiki.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; }

        ///  TODO: Make it so you cannot delete the home page (deleting the home page will cause a 404)
        ///  or re-factor to make the home page dynamic or configurable.
        public async Task<IActionResult> OnGetAsync(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            if (slug == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.SingleOrDefaultAsync(m => m.Slug == slug);

            if (Article != null)
            {
                _context.Articles.Remove(Article);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/All");
        }
    }
}

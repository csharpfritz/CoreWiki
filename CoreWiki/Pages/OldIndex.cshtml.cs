using CoreWiki.Data;
using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IList<Article> Article { get; set; }

		public async Task OnGetAsync()
		{
			Article = await _context.Articles.ToListAsync();
		}
	}
}

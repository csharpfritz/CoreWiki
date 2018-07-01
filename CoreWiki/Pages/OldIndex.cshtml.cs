using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class IndexModel : PageModel
	{
		private readonly IApplicationDbContext _context;

		public IndexModel(IApplicationDbContext context)
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

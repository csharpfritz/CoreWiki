using CoreWiki.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class LatestChangesModel : PageModel
	{
		private readonly IApplicationDbContext _context;

		public LatestChangesModel(IApplicationDbContext context)
		{
			_context = context;
		}

		public IList<Article> Article { get; set; }

		public async Task OnGetAsync()
		{
			Article = await _context.Articles.OrderByDescending(a => a.Published).Take(10).ToListAsync();
		}
	}
}

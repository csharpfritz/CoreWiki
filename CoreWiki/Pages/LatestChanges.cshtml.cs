using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class LatestChangesModel : PageModel
	{
		private readonly IArticleRepository _articleRepo;

		public LatestChangesModel(IArticleRepository articleRepo)
		{
			_articleRepo = articleRepo;
		}


		public IList<Article> Articles { get; set; }


		public async Task OnGetAsync()
		{
			Articles = await _articleRepo.GetLatestArticles(10);
		}
	}
}

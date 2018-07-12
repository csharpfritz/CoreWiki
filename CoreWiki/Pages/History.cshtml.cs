using CoreWiki.Data.Data.Interfaces;
using CoreWiki.Data.Models;
using CoreWiki.Helpers;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class HistoryModel : PageModel
	{

		private readonly IArticleRepository _articleRepo;

		public HistoryModel(IArticleRepository articleRepo)
		{
			_articleRepo = articleRepo;
		}


		public Article Article { get; private set; }

		[BindProperty()]
		public string[] Compare { get; set; }

		public SideBySideDiffModel DiffModel { get; set; }

		public async Task<IActionResult> OnGet(string slug)
		{

			if (string.IsNullOrEmpty(slug))
			{
				return NotFound();
			}

			Article = await _articleRepo.GetArticleWithHistoriesBySlug(slug);

			if (Article == null)
			{
				return new ArticleNotFoundResult();
			}

			return Page();

		}

		public async Task<IActionResult> OnPost(string slug)
		{

			Article = await _articleRepo.GetArticleWithHistoriesBySlug(slug);

			var histories = Article.History
				.Where(h => Compare.Any(c => c == h.Version.ToString()))
				.OrderBy(h => h.Version)
				.ToArray();


			this.DiffModel = new SideBySideDiffBuilder(new DiffPlex.Differ())
				.BuildDiffModel(histories[0].Content, histories[1].Content);

			return Page();

		}

	}
}

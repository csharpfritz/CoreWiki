using CoreWiki.Core.Interfaces;
using CoreWiki.Models;
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


		public ArticleHistoryDTO Article { get; private set; }

		[BindProperty()]
		public string[] Compare { get; set; }

		public SideBySideDiffModel DiffModel { get; set; }

		public async Task<IActionResult> OnGet(string slug)
		{

			if (string.IsNullOrEmpty(slug))
			{
				return NotFound();
			}

			var article = await _articleRepo.GetArticleWithHistoriesBySlug(slug);

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			var histories = (
				from history in article.History
				select new ArticleHistoryDetailDTO
				{
					AuthorName = history.AuthorName,
					Version = history.Version,
					Published = history.Published,
				}
			).ToList();

			Article = new ArticleHistoryDTO
			{
				Topic = article.Topic,
				Version = article.Version,
				AuthorName = article.AuthorName,
				Published = article.Published,
				History = histories
			};

			return Page();
		}

		public async Task<IActionResult> OnPost(string slug)
		{

			var article = await _articleRepo.GetArticleWithHistoriesBySlug(slug);

			var histories = article.History
				.Where(h => Compare.Any(c => c == h.Version.ToString()))
				.OrderBy(h => h.Version)
				.ToArray();

			this.DiffModel = new SideBySideDiffBuilder(new DiffPlex.Differ())
				.BuildDiffModel(histories[0].Content ?? "", histories[1].Content ?? "");

			Article = new ArticleHistoryDTO
			{
				Topic = article.Topic,
				Version = article.Version,
				AuthorName = article.AuthorName,
				Published = article.Published
			};

			return Page();

		}

	}
}

using CoreWiki.ViewModels;
using CoreWiki.Helpers;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Queries;
using MediatR;

namespace CoreWiki.Pages
{
	public class HistoryModel : PageModel
	{
		private readonly IMediator _mediator;

		public HistoryModel(IMediator mediator)
		{
			_mediator = mediator;
		}

		public ArticleHistory Article { get; private set; }

		[BindProperty()]
		public string[] Compare { get; set; }

		public SideBySideDiffModel DiffModel { get; set; }

		public async Task<IActionResult> OnGet(string slug)
		{

			if (string.IsNullOrEmpty(slug))
			{
				return NotFound();
			}

			var qry = new GetArticleWithHistoriesBySlugQuery(slug);

			var article = await _mediator.Send(qry);

			if (article == null)
			{
				return new ArticleNotFoundResult();
			}

			//todo: use automapper
			var histories = (
				from history in article.History
				select new ArticleHistoryDetailDTO
				{
					AuthorName = history.AuthorName,
					Version = history.Version,
					Published = history.Published,
				}
			).ToList();

			Article = new ArticleHistory
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
			var qry = new GetArticleWithHistoriesBySlugQuery(slug);

			var article = await _mediator.Send(qry);

			var histories = article.History
				.Where(h => Compare.Any(c => c == h.Version.ToString()))
				.OrderBy(h => h.Version)
				.ToArray();

			this.DiffModel = new SideBySideDiffBuilder(new DiffPlex.Differ())
				.BuildDiffModel(histories[0].Content ?? "", histories[1].Content ?? "");

			Article = new ArticleHistory
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

using AutoMapper;
using CoreWiki.Application.Articles.Reading.Queries;
using CoreWiki.Helpers;
using CoreWiki.ViewModels;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class HistoryModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public HistoryModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public ArticleHistory Article { get; private set; }

		[BindProperty()]
		public IEnumerable<string> Compare { get; set; }

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

			Article = _mapper.Map<ArticleHistory>(article);

			return Page();
		}

		public async Task<IActionResult> OnPost(string slug)
		{
			if (Compare.Count() < 2)
			{
				return Page();
			}

			var qry = new GetArticleWithHistoriesBySlugQuery(slug);

			var article = await _mediator.Send(qry);

			var histories = article.History
				.Where(h => Compare.Any(c => c == h.Version.ToString()))
				.OrderBy(h => h.Version)
				.ToArray();

			DiffModel = new SideBySideDiffBuilder(new DiffPlex.Differ())
				.BuildDiffModel(histories[0].Content ?? "", histories[1].Content ?? "");

			Article = _mapper.Map<ArticleHistory>(article);

			return Page();
		}
	}
}

using CoreWiki.Core.Domain;
using CoreWiki.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Queries;
using CoreWiki.Application.Articles.Search.Queries;
using MediatR;

namespace CoreWiki.Pages
{
	public class SearchModel : PageModel
	{
		private readonly IMediator _mediator;
		public SearchResult<ArticleSummary> SearchResult;
		private const int ResultsPerPage = 10;

		public SearchModel(IMediator mediator)
		{
			_mediator = mediator;
		}

		public string RequestedPage => Request.Path.Value.ToLowerInvariant().Substring(1);

		public async Task<IActionResult> OnGetAsync([FromQuery(Name = "Query")]string query = "", [FromQuery(Name ="PageNumber")]int pageNumber = 1)
		{
			if (string.IsNullOrEmpty(query))
			{
				return Page();
			}
			var qry = new SearchArticlesQuery(query,
				pageNumber,
				ResultsPerPage);
			var result = await _mediator.Send(qry);

			//todo: use automapper
			SearchResult = new SearchResult<ArticleSummary>
			{
				Query = result.Query,
				TotalResults = result.TotalResults,
				ResultsPerPage = result.ResultsPerPage,
				CurrentPage = result.CurrentPage,
				Results = (from article in result.Results
					select new ArticleSummary
					{
						Slug = article.Slug,
						Topic = article.Topic,
						Published = article.Published,
						ViewCount = article.ViewCount
					}).ToList()
			};
			//SearchResult.CurrentPage = 1;

			return Page();
		}

		public async Task<IActionResult> OnGetLatestChangesAsync()
		{
			var qry = new GetLatestArticlesQuery(10);
			var results = await _mediator.Send(qry);

			SearchResult = new SearchResult<ArticleSummary>
			{
				Results = (from article in results
									 select new ArticleSummary
									 {
										 Slug = article.Slug,
										 Topic = article.Topic,
										 Published = article.Published,
										 ViewCount = article.ViewCount
									 }).ToList(),
				ResultsPerPage = 11,
				CurrentPage = 1
			};
			SearchResult.TotalResults = SearchResult.Results.Count;
			return Page();
		}
	}
}

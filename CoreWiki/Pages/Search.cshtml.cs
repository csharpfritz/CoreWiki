using CoreWiki.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Queries;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Application.Articles.Search.Queries;
using MediatR;
using AutoMapper;

namespace CoreWiki.Pages
{
	public class SearchModel : PageModel
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		public SearchResultDto<ArticleSummary> SearchResult;
		private const int ResultsPerPage = 10;

		public SearchModel(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public string RequestedPage => Request.Path.Value.ToLowerInvariant().Substring(1);

		public async Task<IActionResult> OnGetAsync([FromQuery(Name = "Query")]string query = "", [FromQuery(Name ="PageNumber")]int pageNumber = 1)
		{
			if (string.IsNullOrEmpty(query))
			{
				return Page();
			}
			var qry = new SearchArticlesQuery(query, pageNumber, ResultsPerPage);
			var result = await _mediator.Send(qry);

			SearchResult = _mapper.Map<SearchResultDto<ArticleSummary>>(result);

			return Page();
		}

		public async Task<IActionResult> OnGetLatestChangesAsync()
		{
			var qry = new GetLatestArticlesQuery(10);
			var results = await _mediator.Send(qry);

			SearchResult = _mapper.Map<SearchResultDto<ArticleSummary>>(results);
			SearchResult.ResultsPerPage = 11;
			SearchResult.CurrentPage = 1;

			return Page();
		}
	}
}

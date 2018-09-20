using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Search.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Search.Queries
{
	class SearchArticlesHandler: IRequestHandler<SearchArticlesQuery, SearchResultDto<ArticleSearchDto>>
	{
		private readonly IArticlesSearchEngine _articlesSearchEngine;

		public SearchArticlesHandler(IArticlesSearchEngine articlesSearchEngine)
		{
			_articlesSearchEngine = articlesSearchEngine;
		}
		public Task<SearchResultDto<ArticleSearchDto>> Handle(SearchArticlesQuery request, CancellationToken cancellationToken)
		{
			return _articlesSearchEngine.SearchAsync(request.Query, request.PageNumber, request.ResultsPerPage);
		}
	}
}

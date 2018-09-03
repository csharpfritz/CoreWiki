using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class SearchArticlesQuery: IRequest<SearchResult<ArticleReadingDto>>
	{
		public string Query { get; }
		public int PageNumber { get; }
		public int ResultsPerPage { get; }

		public SearchArticlesQuery(string query, int pageNumber, int resultsPerPage)
		{
			Query = query;
			PageNumber = pageNumber;
			ResultsPerPage = resultsPerPage;
		}
	}
}

using AutoMapper;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Search.Impl
{
	public class ArticlesDbSearchEngine : IArticlesSearchEngine
	{
		private readonly ISearchProvider<Article> _searchProvider;
		private readonly IMapper _mapper;

		public ArticlesDbSearchEngine(ISearchProvider<Article> searchProvider, IMapper mapper)
		{
			_searchProvider = searchProvider;
			_mapper = mapper;
		}

		public async Task<SearchResultDto<ArticleSearchDto>> SearchAsync(string query, int pageNumber, int resultsPerPage)
		{
			var filteredQuery = query.Trim();
			var (articles, totalFound) = await _searchProvider.SearchAsync(filteredQuery, pageNumber, resultsPerPage).ConfigureAwait(false);

			// TODO maybe make this searchproviders problem
			var total = int.TryParse(totalFound.ToString(), out var inttotal);
			if (!total)
			{
				inttotal = int.MaxValue;
			}

			return _mapper.CreateArticleResultDTO(filteredQuery, articles, pageNumber, resultsPerPage, inttotal);
		}
	}

	internal static class SearchResultFactory
	{
		internal static SearchResultDto<ArticleSearchDto> CreateArticleResultDTO(this IMapper mapper, string query, IEnumerable<Article> articles, int currenPage, int resultsPerPage, int totalResults)
		{
			var results = new List<Article>();
			if (articles?.Any() == true)
			{
				results = articles.ToList();
			}
			var result = new SearchResult<Article>
			{
				Query = query,
				Results = results,
				CurrentPage = currenPage,
				ResultsPerPage = resultsPerPage,
				TotalResults = totalResults
			};

			return mapper.Map<SearchResultDto<ArticleSearchDto>>(result);
		}
	}
}

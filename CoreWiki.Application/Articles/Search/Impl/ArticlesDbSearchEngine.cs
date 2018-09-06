using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;

namespace CoreWiki.Application.Articles.Search.Impl
{
	public class ArticlesDbSearchEngine : IArticlesSearchEngine
	{

		private readonly IArticleRepository _articleRepo;
		private readonly IMapper _mapper;

		public ArticlesDbSearchEngine(IArticleRepository articleRepo, IMapper mapper)
		{
			_articleRepo = articleRepo;
			_mapper = mapper;
		}

		public async Task<SearchResult<ArticleSearchDto>> SearchAsync(string query, int pageNumber, int resultsPerPage)
		{
			var filteredQuery = query.Trim();
			var offset = (pageNumber - 1) * resultsPerPage;

			(IEnumerable<Article> articles, int totalFound) searchQueryResult =
				_articleRepo.GetArticlesForSearchQuery(filteredQuery, offset, resultsPerPage);
			var articles = searchQueryResult.articles;

			return new SearchResult<ArticleSearchDto>
			{
				Query = filteredQuery,
				Results = _mapper.Map<List<ArticleSearchDto>>(articles),
				CurrentPage = pageNumber,
				ResultsPerPage = resultsPerPage,
				TotalResults = searchQueryResult.totalFound
			};
		}
	}
}

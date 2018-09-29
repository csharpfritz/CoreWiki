using AutoMapper;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using System.Linq;
using System.Threading.Tasks;

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

		public async Task<SearchResultDto<ArticleSearchDto>> SearchAsync(string query, int pageNumber, int resultsPerPage)
		{
			var filteredQuery = query.Trim();
			var offset = (pageNumber - 1) * resultsPerPage;

			var (articles, totalFound) = _articleRepo.GetArticlesForSearchQuery(filteredQuery, offset, resultsPerPage);

			var result = new SearchResult<Article>
			{
				Query = filteredQuery,
				Results = articles.ToList(),
				CurrentPage = pageNumber,
				ResultsPerPage = resultsPerPage,
				TotalResults = totalFound
			};

			return _mapper.Map<SearchResultDto<ArticleSearchDto>>(result);
		}
	}
}

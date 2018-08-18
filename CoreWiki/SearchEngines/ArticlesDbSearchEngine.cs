using CoreWiki.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Core.Interfaces;
using CoreWiki.Core.Domain;

namespace CoreWiki.SearchEngines
{
	public class ArticlesDbSearchEngine : IArticlesSearchEngine
	{

		private readonly IArticleRepository _articleRepo;

		public ArticlesDbSearchEngine(IArticleRepository articleRepo)
		{
			_articleRepo = articleRepo;
		}

		public async Task<SearchResult<Article>> SearchAsync(string query, int pageNumber, int resultsPerPage)
		{
			var filteredQuery = query.Trim();
			var offset = (pageNumber - 1) * resultsPerPage;

			var dbQuery = _articleRepo.GetArticlesForSearchQuery(filteredQuery);

			var totalResults = await dbQuery.CountAsync();

			var articles = await dbQuery
				.Skip(offset)
				.Take(resultsPerPage)
				.OrderByDescending(a => a.ViewCount)
				.ToListAsync();

			return new SearchResult<Core.Domain.Article>
			{
				Query = filteredQuery,
				Results = articles,
				CurrentPage = pageNumber,
				ResultsPerPage = resultsPerPage,
				TotalResults = totalResults
			};
		}
	}
}

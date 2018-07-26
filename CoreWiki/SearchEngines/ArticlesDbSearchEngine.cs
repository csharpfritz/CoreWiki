using CoreWiki.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Data.Data.Interfaces;

namespace CoreWiki.SearchEngines
{
	public class ArticlesDbSearchEngine : IArticlesSearchEngine
	{

		private readonly IArticleRepository _articleRepo;

		public ArticlesDbSearchEngine(IArticleRepository articleRepo)
		{
			_articleRepo = articleRepo;
		}

		public async Task<SearchResult<ArticleSummaryDTO>> SearchAsync(string query, int pageNumber, int resultsPerPage)
		{
			var filteredQuery = query.Trim();
			var offset = (pageNumber - 1) * resultsPerPage;

			var dbQuery = _articleRepo.GetArticlesForSearchQuery(filteredQuery);

			var totalResults = await dbQuery.CountAsync();

			var articles = from article in await dbQuery
				.Skip(offset)
				.Take(resultsPerPage)
				.OrderByDescending(a => a.ViewCount)
				.ToListAsync()
				select new ArticleSummaryDTO
			{
				Slug = article.Slug,
				Topic = article.Topic,
				Published = article.Published,
				ViewCount = article.ViewCount
			};;

			return new SearchResult<ArticleSummaryDTO>
			{
				Query = filteredQuery,
				Results = articles.ToList(),
				CurrentPage = pageNumber,
				ResultsPerPage = resultsPerPage,
				TotalResults = totalResults
			};
		}
	}
}

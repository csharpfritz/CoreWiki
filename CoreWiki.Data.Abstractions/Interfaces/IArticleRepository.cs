using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;

namespace CoreWiki.Data.Abstractions.Interfaces
{
	public interface IArticleRepository : IDisposable
	{

		Task<Article> GetArticleBySlug(string articleSlug);

		Task<Article> GetArticleWithCommentsById(int articleId);

		Task<Article> GetArticleWithHistoriesBySlug(string articleSlug);

		Task<Article> GetArticleById(int articleId);

		Task<IEnumerable<Article>> GetAllArticlesPaged(int pageSize, int pageNumber);

		Task<List<Article>> GetLatestArticles(int numOfArticlesToGet);

		Task<int> GetTotalPagesOfArticles(int pageSize);

		Task<Article> CreateArticleAndHistory(Article article);

		(IEnumerable<Article>, int) GetArticlesForSearchQuery(string filteredQuery, int offset, int resultsPerPage);

		Task<bool> IsTopicAvailable(string articleSlug, int articleId);

		Task<bool> Exists(int id);

		Task Update(Article article);

		Task IncrementViewCount(string slug);

		Task<Article> Delete(string slug);
	}
}

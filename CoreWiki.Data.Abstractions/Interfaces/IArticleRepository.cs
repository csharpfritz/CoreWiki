using CoreWiki.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Data.Abstractions.Interfaces
{
	public interface IArticleRepository : IDisposable
	{

		Task<Article> GetArticleBySlug(string articleSlug);

		Task<Article> GetArticleWithHistoriesBySlug(string articleSlug);

		Task<Article> GetArticleById(int articleId);

		Task<List<Article>> GetLatestArticles(int numOfArticlesToGet);

		Task<Article> CreateArticleAndHistory(Article article);

		Task<(IEnumerable<Article> articles, int totalFound)> GetArticlesForSearchQuery(string filteredQuery, int offset, int resultsPerPage);

		Task<bool> IsTopicAvailable(string articleSlug, int articleId);

		Task<bool> Exists(int id);

		Task Update(Article article);

		Task IncrementViewCount(string slug);

		Task<Article> Delete(string slug);
	}
}

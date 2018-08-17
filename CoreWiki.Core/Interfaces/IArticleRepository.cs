using CoreWiki.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Core.Interfaces
{
	public interface IArticleRepository : IDisposable
	{

		Task<Article> GetArticleBySlug(string articleSlug);

		Task<Article> GetArticleByComment(Comment comment);

		Task<Article> GetArticleWithHistoriesBySlug(string articleSlug);

		Task<Article> GetArticleById(int articleId);

		Task<IEnumerable<Article>> GetAllArticlesPaged(int pageSize, int pageNumber);

		Task<List<Article>> GetLatestArticles(int numOfArticlesToGet);

		Task<int> GetTotalPagesOfArticles(int pageSize);

		Task<Article> CreateArticleAndHistory(Article article);

		IQueryable<Article> GetArticlesForSearchQuery(string filteredQuery);

		Task<bool> IsTopicAvailable(string articleSlug, int articleId);

		Task<bool> Exists(int id);

		Task Update(Article article);

		Task IncrementViewCount(string slug);

	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;

namespace CoreWiki.Application.Articles.Search
{
	/// <summary>
	/// Proxy pattern: Using a middleman to extend functionality of original class but not changing it.
	/// Ex caching, when you dont want/need to muddle the original class with logic for another resposibility,
	/// Or here when we want to extend our app with a searchengine, wich needs indexing. The repository dont need to know we do indexing somewere else
	/// </summary>
	public class ArticleRepositorySearchIndexingProxy : IArticleRepository
	{
		private readonly ISearchProvider<Article> _searchProvider;
		private readonly IArticleRepository _repository;

		public ArticleRepositorySearchIndexingProxy(ISearchProvider<Article> searchProvider, Func<int, IArticleRepository> repository)
		{
			_searchProvider = searchProvider;
			_repository = repository(1);
		}

		public Task<Article> CreateArticleAndHistory(Article article)
		{
			_searchProvider.IndexElementsAsync(article);
			return _repository.CreateArticleAndHistory(article);
		}

		public Task<Article> Delete(string slug)
		{
			return _repository.Delete(slug);
		}

		public void Dispose()
		{
			_repository.Dispose();
		}

		// GetFrom Search
		public Task<bool> Exists(int id)
		{
			return _repository.Exists(id);
		}

		public Task<Article> GetArticleById(int articleId)
		{
			return _repository.GetArticleById(articleId);
		}

		public Task<Article> GetArticleBySlug(string articleSlug)
		{
			return _repository.GetArticleBySlug(articleSlug);
		}

		// TODO: Search should not be a part of repository
		public (IEnumerable<Article> articles, int totalFound) GetArticlesForSearchQuery(string filteredQuery, int offset, int resultsPerPage)
		{
			return _repository.GetArticlesForSearchQuery(filteredQuery, offset, resultsPerPage);
		}

		public Task<Article> GetArticleWithHistoriesBySlug(string articleSlug)
		{
			return _repository.GetArticleWithHistoriesBySlug(articleSlug);
		}

		// TODO: get from search
		public Task<List<Article>> GetLatestArticles(int numOfArticlesToGet)
		{
			return _repository.GetLatestArticles(numOfArticlesToGet);
		}

		//TODO: update search?
		public Task IncrementViewCount(string slug)
		{
			return _repository.IncrementViewCount(slug);
		}

		//TODO: Topic from Search
		public Task<bool> IsTopicAvailable(string articleSlug, int articleId)
		{
			return _repository.IsTopicAvailable(articleSlug, articleId);
		}

		public Task Update(Article article)
		{
			_searchProvider.IndexElementsAsync(article);
			return _repository.Update(article);
		}
	}
}

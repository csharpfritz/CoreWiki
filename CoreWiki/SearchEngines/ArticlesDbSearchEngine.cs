using System.Collections.Generic;
using System.Linq;
using CoreWiki.Models;

namespace CoreWiki.SearchEngines
{
	public class ArticlesDbSearchEngine : IArticlesSearchEngine
	{
		private readonly ApplicationDbContext _context;

		public ArticlesDbSearchEngine(ApplicationDbContext context)
		{
			_context = context;
		}

		public SearchResult Search(string query)
		{
			var filteredQuery = query.Trim();

			var articles = _context.Articles.Where(article =>
				article.Topic.ToUpper().Contains(filteredQuery.ToUpper()) ||
				article.Content.ToUpper().Contains(filteredQuery.ToUpper())
			).OrderBy(a => a.Topic).ToList();

			return new SearchResult
			{
				Query = filteredQuery,
				Articles = articles
			};
		}
	}
}

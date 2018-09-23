using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using CoreWiki.Data.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreWiki.Application.Articles.Search.Impl
{
	/// <summary>
	/// When using local DB convert Generic search to Concrete Articlesearch
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LocalDbArticleSearchProviderAdapter<T> : ISearchProvider<T> where T : Article
	{
		private readonly ILogger _logger;
		private readonly IArticleRepository _articleRepo;

		public LocalDbArticleSearchProviderAdapter(ILogger<LocalDbArticleSearchProviderAdapter<T>> logger, IArticleRepository articleRepo)
		{
			_logger = logger;
			_articleRepo = articleRepo;
		}

		public Task<int> IndexElementsAsync(params T[] items)
		{
			// For LocalDB DB itself is responsible for "Indexing"
			return Task.Run(() => items.Length);
		}

		public async Task<(IEnumerable<T> results, long total)> SearchAsync(string Query, int pageNumber, int resultsPerPage)
		{
			var offset = (pageNumber - 1) * resultsPerPage;
			var (articles, totalFound) = _articleRepo.GetArticlesForSearchQuery(Query, offset, resultsPerPage);

			var supportedType = articles.GetType().GetGenericArguments()[0];
			if (typeof(T) == supportedType)
			{
				var tlist = articles.Cast<T>();
				return (results: tlist, total: totalFound);
			}

			_logger.LogWarning($"{nameof(SearchAsync)}: Only supports search for {nameof(supportedType)} but asked for {typeof(T).FullName}");
			return (Enumerable.Empty<T>(), 0);
		}
	}
}

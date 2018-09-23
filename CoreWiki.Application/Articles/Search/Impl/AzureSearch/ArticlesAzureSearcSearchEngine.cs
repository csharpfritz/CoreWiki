using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Logging;

namespace CoreWiki.Application.Articles.Search.AzureSearch
{
	/// <summary>
	/// Tutorial here: https://github.com/Azure-Samples/search-dotnet-getting-started/blob/master/DotNetHowTo/DotNetHowTo/Program.cs
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class AzureSearchProvider<T> : ISearchProvider<T> where T : class
	{
		private readonly ILogger _logger;
		private readonly IAzureSearchClient _searchClient;
		private readonly ISearchIndexClient _myclient;

		public AzureSearchProvider(ILogger<AzureSearchProvider<T>> logger, IAzureSearchClient searchClient)
		{
			_logger = logger;
			_searchClient = searchClient;
			_myclient = _searchClient.GetSearchClient<T>();
		}

		public async Task<int> IndexElementsAsync(bool clearIndex = false, params T[] items)
		{
			if (clearIndex)
			{
				DeleteCurrentItems();
			}

			var action = items.Select(IndexAction.MergeOrUpload);
			var job = new IndexBatch<T>(action);

			try
			{
				var res = await _searchClient.CreateServiceClient<T>().Documents.IndexAsync<T>(job).ConfigureAwait(false);
				return res.Results.Count;
			}
			catch (IndexBatchException e)
			{
				// Sometimes when your Search service is under load, indexing will fail for some of the documents in
				// the batch. Depending on your application, you can take compensating actions like delaying and
				// retrying. For this simple demo, we just log the failed document keys and continue.

				var failedElements = e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key);
				_logger.LogError(e, "Failed to index some of the documents", failedElements);
				return items.Length - failedElements.Count();
			}
		}

		private void DeleteCurrentItems()
		{
			// TODO
		}

		public async Task<(IEnumerable<T> results, long total)> SearchAsync(string Query, int pageNumber, int resultsPerPage)
		{
			var offset = (pageNumber - 1) * resultsPerPage;
			var parameters = new SearchParameters()
			{
				IncludeTotalResultCount = true,
				Top = resultsPerPage,
				Skip = offset,
			};
			try
			{
				var res = await _myclient.Documents.SearchAsync(Query, parameters).ConfigureAwait(false);

				var total = res.Count.GetValueOrDefault();
				var list = res.Results;
				//TODO: map results

				return (results: null, total: total);
			}
			catch (System.Exception e)
			{
				_logger.LogCritical(e, $"{nameof(SearchAsync)} Search failed horribly, you should check it out");
				return (results: null, total: 0);
			}
		}
	}
}

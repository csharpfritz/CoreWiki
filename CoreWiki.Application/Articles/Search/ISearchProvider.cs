using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Search
{
	public interface ISearchProvider<T> where T : class
	{
		Task<(IEnumerable<T> results, long total)> SearchAsync(string Query, int pageNumber, int resultsPerPage);

		Task<int> IndexElementsAsync(bool clearIndex = false, params T[] items);
	}
}

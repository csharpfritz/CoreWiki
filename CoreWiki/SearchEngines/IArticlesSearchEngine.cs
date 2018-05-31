using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWiki.Models;

namespace CoreWiki.SearchEngines
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

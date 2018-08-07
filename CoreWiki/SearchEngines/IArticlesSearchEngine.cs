using CoreWiki.Models;
using System.Threading.Tasks;

namespace CoreWiki.SearchEngines
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult<Article>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

using CoreWiki.Data.Models;
using System.Threading.Tasks;

namespace CoreWiki.SearchEngines
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult<ArticleSummaryDTO>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

using CoreWiki.Core.Domain;
using System.Threading.Tasks;

namespace CoreWiki.Core.Interfaces
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult<Domain.Article>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

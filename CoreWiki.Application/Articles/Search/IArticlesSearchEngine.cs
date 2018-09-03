using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Search
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult<ArticleReadingDto>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

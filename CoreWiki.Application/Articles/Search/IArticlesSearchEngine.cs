using CoreWiki.Application.Articles.Search.Dto;
using System.Threading.Tasks;

namespace CoreWiki.Application.Articles.Search
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResultDto<ArticleSearchDto>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

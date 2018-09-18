using System.Threading.Tasks;
using CoreWiki.Application.Articles.Search.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Search
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResultDto<ArticleSearchDto>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

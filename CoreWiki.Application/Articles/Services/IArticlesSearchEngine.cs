using System.Threading.Tasks;
using CoreWiki.Application.Articles.Services.Dto;
using CoreWiki.Core.Domain;

namespace CoreWiki.Application.Articles.Services
{
	public interface IArticlesSearchEngine
	{
		Task<SearchResult<ArticleReadingDto>> SearchAsync(string query, int pageNumber, int resultsPerPage);
	}
}

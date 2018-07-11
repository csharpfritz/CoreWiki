using CoreWiki.Data.Models;
using CoreWiki.SearchEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWiki.Pages
{
	public class SearchModel : PageModel
	{
		public SearchResult<Article> SearchResult;
		private readonly IArticlesSearchEngine _articlesSearchEngine;
		private const int ResultsPerPage = 10;

		public SearchModel(IArticlesSearchEngine articlesSearchEngine)
		{
			_articlesSearchEngine = articlesSearchEngine;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			var isQueryPresent = TryGetSearchQuery(out var query);

			if (isQueryPresent)
			{
				SearchResult = await _articlesSearchEngine.SearchAsync(
					query,
					GetPageNumberOrDefault(),
					ResultsPerPage
				);
			}

			return Page();
		}

		private bool TryGetSearchQuery(out string query)
		{
			var isQueryParamPresent = Request.Query.TryGetValue("Query", out var queryParams);

			if (isQueryParamPresent && !string.IsNullOrEmpty(queryParams.First()))
			{
				query = queryParams.First();
				return true;
			}

			query = "";
			return false;
		}

		private int GetPageNumberOrDefault()
		{
			var isPageParamPresent = Request.Query.TryGetValue("PageNumber", out var pageParams);

			if (isPageParamPresent && !string.IsNullOrEmpty(pageParams.First()))
			{
				var isValidNumber = int.TryParse(pageParams.First(), out var pageNumber);
				return isValidNumber ? pageNumber : 1;
			}

			return 1;
		}
	}

}

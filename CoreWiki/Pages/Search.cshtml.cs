using CoreWiki.Data.Data.Interfaces;
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
		private readonly IArticleRepository _repository;
		private const int ResultsPerPage = 10;

		public SearchModel(IArticlesSearchEngine articlesSearchEngine, IArticleRepository repository)
		{
			_articlesSearchEngine = articlesSearchEngine;
			_repository = repository;
		}

		public string RequestedPage {  get { return Request.Path.Value.ToLowerInvariant().Substring(1); } }

		public async Task<IActionResult> OnGetAsync([FromQuery(Name = "Query")]string query = "", [FromQuery(Name ="PageNumber")]int pageNumber = 1)
		{
			if (!string.IsNullOrEmpty(query))
			{
				SearchResult = await _articlesSearchEngine.SearchAsync(
					query,
					pageNumber,
					ResultsPerPage
				);
				SearchResult.CurrentPage = 1;
			}

			return Page();
		}

		public async Task<IActionResult> OnGetLatestChangesAsync()
		{
			SearchResult = new SearchResult<Article>
			{
				Results = await _repository.GetLatestArticles(10),
				ResultsPerPage = 11,
				CurrentPage = 1
			};
			SearchResult.TotalResults = SearchResult.Results.Count();
			return Page();
		}
	}

}

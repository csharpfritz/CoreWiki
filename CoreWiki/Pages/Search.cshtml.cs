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
		public SearchResult<ArticleSummaryDTO> SearchResult;
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
			if (!string.IsNullOrWhiteSpace(query))
			{
				var result = await _articlesSearchEngine.SearchAsync(
					query,
					pageNumber,
					ResultsPerPage
				);

				SearchResult = new SearchResult<ArticleSummaryDTO>()
				{
					Query = result.Query,
					TotalResults = result.TotalResults,
					ResultsPerPage = result.ResultsPerPage,
					CurrentPage = result.CurrentPage,
					Results = (from article in result.Results
						select new ArticleSummaryDTO
						{
							Slug = article.Slug,
							Topic = article.Topic,
							Published = article.Published,
							ViewCount = article.ViewCount
						}).ToList()
				};
			}

			return Page();

		}

	}

}

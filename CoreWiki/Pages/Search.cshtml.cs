using System.Collections.Generic;
using System.Linq;
using CoreWiki.Models;
using CoreWiki.SearchEngines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreWiki.Pages
{
	public class SearchModel : PageModel
	{
		public SearchResult SearchResult;
		private readonly IArticlesSearchEngine _articlesSearchEngine;

		public SearchModel(IArticlesSearchEngine articlesSearchEngine)
		{
			_articlesSearchEngine = articlesSearchEngine;
		}

		public IActionResult OnGet()
		{
			var isQueryPresent = Request.Query.TryGetValue("q", out var query);

			if (isQueryPresent && !string.IsNullOrEmpty(query.First()))
			{
				SearchResult = _articlesSearchEngine.Search(query.First());
			}

			return Page();
		}
	}

}

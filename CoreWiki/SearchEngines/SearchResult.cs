using System.Collections.Generic;
using CoreWiki.Models;

namespace CoreWiki.SearchEngines
{
	public class SearchResult
	{
		public string Query { get; set; }

		public List<Article> Articles { get; set; }

		public int TotalResults => Articles.Count;
	}
}

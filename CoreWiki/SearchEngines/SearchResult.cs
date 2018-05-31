using System;
using System.Collections.Generic;
using CoreWiki.Models;

namespace CoreWiki.SearchEngines
{
	public class SearchResult<T>
	{
		public string Query { get; set; }

		public List<T> Results { get; set; }

		public int TotalResults { get; set; }

		public int ResultsPerPage { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages => (int) Math.Ceiling((decimal)TotalResults / ResultsPerPage);
	}
}

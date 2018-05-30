using System.Collections.Generic;
using CoreWiki.Models;

namespace CoreWiki.SearchEngines
{
	public interface IArticlesSearchEngine
	{
		SearchResult Search(string query);
	}
}

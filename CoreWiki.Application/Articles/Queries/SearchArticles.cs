using System;
using System.Collections.Generic;
using System.Text;
using CoreWiki.Application.Articles.Services.Dto;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class SearchArticles: IRequest<SearchResult<ArticleReadingDto>>
	{
		public string Query { get; }
		public int PageNumber { get; }
		public int ResultsPerPage { get; }

		public SearchArticles(string query, int pageNumber, int resultsPerPage)
		{
			Query = query;
			PageNumber = pageNumber;
			ResultsPerPage = resultsPerPage;
		}
	}
}

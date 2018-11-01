using System.Collections.Generic;
using CoreWiki.Application.Articles.Reading.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetLatestArticlesQuery: IRequest<List<ArticleReadingDto>>
	{
		public int NumOfArticlesToGet { get; }

		public GetLatestArticlesQuery(int numOfArticlesToGet)
		{
			NumOfArticlesToGet = numOfArticlesToGet;
		}
	}
}

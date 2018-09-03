using System;
using System.Collections.Generic;
using System.Text;
using CoreWiki.Application.Articles.Services.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetLatestArticles: IRequest<List<ArticleReadingDto>>
	{
		public int NumOfArticlesToGet { get; }

		public GetLatestArticles(int numOfArticlesToGet)
		{
			NumOfArticlesToGet = numOfArticlesToGet;
		}
	}
}

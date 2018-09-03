using System;
using System.Collections.Generic;
using System.Text;
using CoreWiki.Application.Articles.Services.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticleWithHistoriesBySlug: IRequest<ArticleReadingDto>
	{
		public string Slug { get; }

		public GetArticleWithHistoriesBySlug(string slug)
		{
			Slug = slug;
		}
	}
}

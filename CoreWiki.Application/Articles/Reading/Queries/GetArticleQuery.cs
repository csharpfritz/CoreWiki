using CoreWiki.Application.Articles.Reading.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetArticleQuery: IRequest<ArticleReadingDto>
	{
		public string Slug { get; }
		public GetArticleQuery(string slug) => Slug = slug;
	}
}

using CoreWiki.Application.Articles.Reading.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Reading.Queries
{
	public class GetArticleByIdQuery : IRequest<ArticleReadingDto>
	{
		public int Id { get; }
		public GetArticleByIdQuery(int id) => Id = id;
	}
}

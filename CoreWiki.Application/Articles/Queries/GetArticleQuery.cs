using CoreWiki.Application.Articles.Services.Dto;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticleQuery: IRequest<ArticleReadingDto>
	{
		public string Slug { get; }
		public GetArticleQuery(string slug) => Slug = slug;
	}

	public class GetArticleByIdQuery : IRequest<ArticleReadingDto>
	{
		public int Id { get; }
		public GetArticleByIdQuery(int id) => Id = id;
	}

	public class GetIsTopicAvailableQuery : IRequest<bool>
	{
		public string Slug { get; set; }
		public int ArticleId { get; set; }
	}
}

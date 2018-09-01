using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticle: IRequest<Article>
	{
		public string Slug { get; }
		public GetArticle(string slug) => Slug = slug;
	}

	public class GetArticleById : IRequest<Article>
	{
		public int Id { get; }
		public GetArticleById(int id) => Id = id;
	}
}

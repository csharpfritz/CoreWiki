using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{
	public class GetArticlesToCreateFromArticleQuery : IRequest<string[]>
	{
		public GetArticlesToCreateFromArticleQuery(string slug) 
		{

			this.Slug = slug;

		}

		public string Slug { get; }
	}

}

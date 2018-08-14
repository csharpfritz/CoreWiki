using MediatR;

namespace CoreWiki.Application.Articles.Queries
{
	public class GetArticle: IRequest<Core.Domain.Article> {

		public GetArticle(string slug)
		{
			Slug = slug;
		}

		public string Slug { get; }

	}


}

using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{
	public class GetArticlesToCreateFromArticleQuery : IRequest<(string,string[])>
	{
		public GetArticlesToCreateFromArticleQuery(int articleId) 
		{

			this.ArticleId = articleId;

		}

		public int ArticleId { get; }
	}

}

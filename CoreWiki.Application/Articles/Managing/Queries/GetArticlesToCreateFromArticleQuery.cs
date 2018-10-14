using CoreWiki.Core.Common;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Queries
{
	public class GetArticlesToCreateFromArticleQuery : IRequest<string[]>
	{
		public GetArticlesToCreateFromArticleQuery(string slug)
		{
			var friendlySlug = UrlHelpers.URLFriendly(slug);
			this.Slug = friendlySlug;
		}

		public string Slug { get; }
	}
}

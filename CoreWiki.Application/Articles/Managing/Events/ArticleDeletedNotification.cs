using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Events
{
	public class ArticleDeletedNotification : INotification
	{
		public Article Article { get; }

		public ArticleDeletedNotification(Article article)
		{
			Article = article;
		}
	}
}

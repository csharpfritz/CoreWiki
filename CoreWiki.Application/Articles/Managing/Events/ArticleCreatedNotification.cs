using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Managing.Events
{
	public class ArticleCreatedNotification : INotification
	{
		public Article Article { get; }

		public ArticleCreatedNotification(Article article)
		{
			Article = article;
		}
	}
}

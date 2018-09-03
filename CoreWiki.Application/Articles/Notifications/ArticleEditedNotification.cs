using System;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Notifications
{
	public class ArticleEditedNotification: INotification
	{
		public Article Editedarticle { get; }

		public ArticleEditedNotification(Article editedarticle)
		{
			Editedarticle = editedarticle;
		}
	}
}

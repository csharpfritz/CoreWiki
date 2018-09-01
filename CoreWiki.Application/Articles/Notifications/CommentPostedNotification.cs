using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Core.Domain;
using MediatR;

namespace CoreWiki.Application.Articles.Notifications
{
	public class CommentPostedNotification : INotification
	{
		public Article Article { get; }
		public Comment Comment { get; }

		public CommentPostedNotification(Article article, Comment comment)
		{
			Article = article;
			Comment = comment;
		}
	}
}

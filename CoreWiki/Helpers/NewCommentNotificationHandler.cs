using System.Threading;
using System.Threading.Tasks;
using CoreWiki.Application.Articles.Reading.Events;
using CoreWiki.Core.Notifications;
using CoreWiki.Data.EntityFramework.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CoreWiki.Helpers
{
	// TODO: Move this to a more suitable location in the project: CoreWiki.Notifications?
	public class NewCommentNotificationHandler : INotificationHandler<CommentPostedNotification>
	{
		private readonly UserManager<CoreWikiUser> _userManager;
		private readonly INotificationService _notificationService;

		public NewCommentNotificationHandler(UserManager<CoreWikiUser> userManager, INotificationService notificationService)
		{
			_userManager = userManager;
			_notificationService = notificationService;
		}

		public async Task Handle(CommentPostedNotification notification, CancellationToken cancellationToken)
		{
			var author = await _userManager.FindByIdAsync(notification.Article.AuthorId.ToString());
			if (author != null)
			{
				await _notificationService.SendNewCommentEmail(author.Email, author.UserName, notification.Comment.DisplayName, notification.Article.Topic, notification.Article.Slug, () => author.CanNotify);
			}
		}
	}
}

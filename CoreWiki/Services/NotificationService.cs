using CoreWiki.Areas.Identity.Data;
using CoreWiki.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IEmailMessageFormatter _emailMessageFormatter;
		private readonly IEmailSender _emailSender;
		private readonly UserManager<CoreWikiUser> _userManager;

		public NotificationService(IEmailMessageFormatter emailMessageFormatter, IEmailSender emailSender, UserManager<CoreWikiUser> userManager)
		{
			_emailMessageFormatter = emailMessageFormatter;
			_emailSender = emailSender;
			_userManager = userManager;
		}

		public async Task<bool> NotifyAuthorNewComment(Article article, Comment comment)
		{
			var author = await _userManager.FindByIdAsync(article.AuthorId.ToString());
			if (author == null) throw new Exception("Author not found");
			if (string.IsNullOrWhiteSpace(author.Email)) return false;

			var model = new
			{
				AuthorName = author.UserName,
				Title = "CoreWiki Notification",
				CommentDisplayName = comment.DisplayName,
				ArticleTitle = article.Topic
			};
			var messageBody = await _emailMessageFormatter.FormatEmailMessage("NewComment", model);

			return await _emailSender.SendEmailAsync(author.Email, "Someone said something about your article", messageBody);
		}
	}
}

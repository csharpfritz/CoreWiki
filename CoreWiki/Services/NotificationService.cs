using CoreWiki.Areas.Identity.Data;
using CoreWiki.Core.Configuration;
using CoreWiki.Core.Notifications;
using CoreWiki.Data.Models;
using CoreWiki.Notifications;
using CoreWiki.Notifications.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public class NotificationService : INotificationService
	{
		private readonly IEmailMessageFormatter _emailMessageFormatter;
		private readonly IEmailNotifier _emailNotifier;
		private readonly UserManager<CoreWikiUser> _userManager;
		private readonly AppSettings _appSettings;

		public NotificationService(
			IEmailMessageFormatter emailMessageFormatter,
			IEmailNotifier emailNotifier,
			UserManager<CoreWikiUser> userManager,
			IOptionsSnapshot<AppSettings> appSettings)
		{
			_emailMessageFormatter = emailMessageFormatter;
			_emailNotifier = emailNotifier;
			_userManager = userManager;
			_appSettings = appSettings.Value;
		}

		public async Task<bool> NotifyAuthorNewComment(CoreWikiUser author, Article article, Comment comment)
		{
			if (!author.CanNotify) return false;
			if (string.IsNullOrWhiteSpace(author.Email)) return false;

			var model = new NewCommentEmailModel()
			{
				AuthorName = author.UserName,
				Title = "CoreWiki Notification",
				CommenterDisplayName = comment.DisplayName,
				ArticleTopic = article.Topic,
				ArticleUrl = GetUrlForArticle(article)
			};
			var messageBody = await _emailMessageFormatter.FormatEmailMessage(TemplateProvider.NewCommentEmail, model);

			return await _emailNotifier.SendEmailAsync(author.Email, "Someone said something about your article", messageBody);
		}

		private string GetUrlForArticle(Article article)
		{
			return $"{_appSettings.Url}{article.Topic}";
		}
	}
}

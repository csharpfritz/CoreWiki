using CoreWiki.Areas.Identity.Data;
using CoreWiki.Configuration;
using CoreWiki.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
			IConfiguration configuration)
		{
			_emailMessageFormatter = emailMessageFormatter;
			_emailNotifier = emailNotifier;
			_userManager = userManager;
			_appSettings = configuration.Get<AppSettings>();
		}

		public async Task<bool> NotifyAuthorNewComment(CoreWikiUser author, Article article, Comment comment)
		{
			if (!author.CanNotify) return false;
			if (string.IsNullOrWhiteSpace(author.Email)) return false;

			var model = new
			{
				AuthorName = author.UserName,
				Title = "CoreWiki Notification",
				CommentDisplayName = comment.DisplayName,
				ArticleTitle = article.Topic,
				ArticleUrl = GetUrlForArticle(article)
			};
			var messageBody = await _emailMessageFormatter.FormatEmailMessage("NewComment", model);

			return await _emailNotifier.SendEmailAsync(author.Email, "Someone said something about your article", messageBody);
		}

		private string GetUrlForArticle(Article article)
		{
			return $"{_appSettings.Url}{article.Topic}";
		}
	}
}

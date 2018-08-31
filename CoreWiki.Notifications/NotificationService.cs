using System;
using CoreWiki.Core.Configuration;
using CoreWiki.Core.Notifications;
using CoreWiki.Notifications.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreWiki.Notifications
{
    public class NotificationService : INotificationService
	{
		private readonly IEmailMessageFormatter _emailMessageFormatter;
		private readonly IEmailNotifier _emailNotifier;
		private readonly AppSettings _appSettings;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
			IEmailMessageFormatter emailMessageFormatter,
			IEmailNotifier emailNotifier,
			IOptionsSnapshot<AppSettings> appSettings,
			ILoggerFactory loggerFactory)
		{
			_emailMessageFormatter = emailMessageFormatter;
			_emailNotifier = emailNotifier;
			_appSettings = appSettings.Value;
		    _logger = loggerFactory.CreateLogger<NotificationService>();
		}

		public async Task<bool> SendConfirmationEmail(string confirmEmail, string userId, string confirmCode)
		{
            _logger.LogInformation("Sending confirmation email");

		    if (string.IsNullOrWhiteSpace(confirmEmail))
		    {
                _logger.LogWarning("Missing parameter {Parameter}, confirmation email not sent", nameof(confirmEmail));
		        return false;
		    }

		    if (string.IsNullOrWhiteSpace(userId))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, confirmation email not sent", nameof(userId));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(confirmCode))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, confirmation email not sent", nameof(confirmCode));
                return false;
		    }

		    try
		    {
                // Confirm code needs to be url encoded for verification to work
                var encodedConfirmCode = UrlEncoder.Default.Encode(confirmCode);
		        var model = new ConfirmationEmailModel()
		        {
		            BaseUrl = _appSettings.Url.ToString(),
		            Title = "CoreWiki Email Confirmation",
		            ReturnUrl = $"{_appSettings.Url}Identity/Account/ConfirmEmail?userId={userId}&code={encodedConfirmCode}",
                    ConfirmEmail = confirmEmail
		        };

                var messageBody = await _emailMessageFormatter.FormatEmailMessage(
		            TemplateProvider.ConfirmationEmailTemplate,
		            model);

		        return await _emailNotifier.SendEmailAsync(
		            confirmEmail,
		            "Please confirm your email address",
		            messageBody);
            }
		    catch (Exception ex)
		    {
		        _logger.LogError(ex, ex.Message);
#if DEBUG
		        throw;
#else
                return false;
#endif
            }
        }

		public async Task<bool> SendForgotPasswordEmail(string accountEmail, string resetToken, Func<bool> canNotifyUser)
		{
		    _logger.LogInformation("Sending password reset email");

		    if (!canNotifyUser())
		    {
		        _logger.LogInformation("User has not consented to receiving emails, email not sent");
            }

            if (string.IsNullOrWhiteSpace(accountEmail))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, password reset email not sent", nameof(accountEmail));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(resetToken))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, password reset email not sent", nameof(resetToken));
                return false;
		    }

            try
            {
                var model = new ForgotPasswordEmailModel()
                {
                    BaseUrl = _appSettings.Url.ToString(),
                    Title = "CoreWiki Password Reset",
                    ReturnUrl = $"{_appSettings.Url}Identity/Account/ResetPassword?code={resetToken}",
                    AccountEmail = accountEmail
                };

                var messageBody = await _emailMessageFormatter.FormatEmailMessage(
					TemplateProvider.ForgotPasswordEmailTemplate,
					model);

			    return await _emailNotifier.SendEmailAsync(
				    accountEmail,
				    "CoreWiki Password Reset",
				    messageBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
#if DEBUG
                throw;
#else
                return false;
#endif
            }
        }

		public async Task<bool> SendNewCommentEmail(string authorEmail, string authorName, string commenterName, string articleTopic, string articleSlug, Func<bool> canNotifyUser)
		{
		    _logger.LogInformation("Sending new comment email");

		    if (!canNotifyUser())
		    {
		        _logger.LogInformation("User has not consented to receiving emails, email not sent");
				return false;
		    }

            if (string.IsNullOrWhiteSpace(authorEmail))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, new comment email not sent", nameof(authorEmail));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(authorName))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, new comment email not sent", nameof(authorName));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(commenterName))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, new comment email not sent", nameof(commenterName));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(articleTopic))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, new comment email not sent", nameof(articleTopic));
                return false;
		    }

		    if (string.IsNullOrWhiteSpace(articleSlug))
		    {
		        _logger.LogWarning("Missing parameter {Parameter}, new comment email not sent", nameof(articleSlug));
                return false;
		    }

		    try
		    {
		        var model = new NewCommentEmailModel()
		        {
		            BaseUrl = _appSettings.Url.ToString(),
		            Title = "CoreWiki Notification",
		            AuthorName = authorName,
		            CommenterDisplayName = commenterName,
		            ArticleTopic = articleTopic,
		            ArticleUrl = $"{_appSettings.Url}{articleSlug}"
		        };

                var messageBody = await _emailMessageFormatter.FormatEmailMessage(
				    TemplateProvider.NewCommentEmailTemplate,
				    model);

			    return await _emailNotifier.SendEmailAsync(
				    authorEmail,
				    "Someone said something about your article",
				    messageBody);
		    }
		    catch (Exception ex)
		    {
		        _logger.LogError(ex, ex.Message);
#if DEBUG
		        throw;
#else
                return false;
#endif
            }
        }
	}
}

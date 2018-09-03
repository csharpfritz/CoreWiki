using CoreWiki.Core.Configuration;
using CoreWiki.Core.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Threading.Tasks;

namespace CoreWiki.Notifications
{
	public class EmailNotifier : IEmailNotifier
	{
		private readonly EmailNotifications _configuration;
		private readonly ILogger _logger;

		public EmailNotifier(IOptionsSnapshot<AppSettings> appSettings, ILoggerFactory loggerFactory)
		{
			_configuration = appSettings.Value.EmailNotifications;
			_logger = loggerFactory.CreateLogger<EmailNotifier>();
		}

		public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
		{
			return await SendEmailAsync(recipientEmail, string.Empty, subject, body);
		}

		public async Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body)
		{
			_logger.LogInformation("Sending email message");

			if (string.IsNullOrWhiteSpace(_configuration.SendGridApiKey))
			{
				_logger.LogWarning($"Missing SendGridApiKey setting in {nameof(EmailNotifications)}");

				return false;
			}

			if (string.IsNullOrWhiteSpace(_configuration.FromEmailAddress))
			{
				_logger.LogWarning($"Missing from FromEmailAddress setting in {nameof(EmailNotifications)}");

				return false;
			}

			if (string.IsNullOrWhiteSpace(recipientEmail))
			{
				_logger.LogWarning("Missing recipient email, email message not sent");

				return false;
			}

			//if (string.IsNullOrWhiteSpace(recipientName))
			//{
			//    _logger.LogWarning("Missing recipient name, email message not sent");

			//    return false;
			//}

			var message = new SendGridMessage();
			var from = new EmailAddress(_configuration.FromEmailAddress, _configuration.FromName);
			var to = new EmailAddress(recipientEmail, recipientName);

			message.SetFrom(from);
			message.AddTo(to);
			message.SetSubject(subject);
			message.AddContent(MimeType.Html, body);

			var client = new SendGridClient(_configuration.SendGridApiKey);

			var response = await client.SendEmailAsync(message);

			_logger.LogInformation($"Sent email form {from.Email} to {to.Email} response {response.StatusCode}");

			return response.StatusCode == HttpStatusCode.OK;
		}
	}
}

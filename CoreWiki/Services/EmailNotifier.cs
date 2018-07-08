using CoreWiki.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoreWiki.Services
{
	public class EmailNotifier : IEmailNotifier
	{
		private readonly EmailNotifications _configuration;
		private readonly ILogger _logger;
		private readonly ISendGridClient _sendGridClient;

		public EmailNotifier(EmailNotifications configuration, ILoggerFactory loggerFactory, ISendGridClient sendGridClient)
		{
			_configuration = configuration;
			_logger = loggerFactory.CreateLogger<EmailNotifier>();
			_sendGridClient = sendGridClient;

			if (!ValidateConfiguration()) throw new ApplicationException("Invalid configuration");

		}

		private bool ValidateConfiguration() {

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
			if (string.IsNullOrWhiteSpace(_configuration.FromName))
			{
				_logger.LogWarning($"Missing from FromName setting in {nameof(EmailNotifications)}");

				return false;
			}

			return true;

		}

		public async Task<bool> SendEmailAsync(string recipientEmail, string subject, string body)
		{
			return await SendEmailAsync(recipientEmail, string.Empty, subject, body);
		}

		public async Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body)
		{

			if (string.IsNullOrWhiteSpace(recipientEmail))
			{
				throw new ArgumentException(nameof(recipientEmail));
			}

			var message = new SendGridMessage();
			var from = new EmailAddress(_configuration.FromEmailAddress, _configuration.FromName);
			var to = new EmailAddress(recipientEmail, recipientName);

			message.SetFrom(from);
			message.AddTo(to);
			message.SetSubject(subject);
			message.AddContent(MimeType.Html, body);

			//var client = new SendGridClient(_configuration.SendGridApiKey);

			var response = await _sendGridClient.SendEmailAsync(message);

			_logger.LogInformation($"Sent email form {from.Email} to {to.Email} response {response.StatusCode}");

			return response.StatusCode == HttpStatusCode.OK;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CoreWiki.Services
{
	public class EmailNotifier : IEmailSender
	{
		private IConfiguration _configuration;

		public EmailNotifier(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmailAsync(string recipientEmail, string subject, string body) {

			// Don't do anything if SendGrid isn't configured
			if (string.IsNullOrEmpty(_configuration["SendGridApiKey"])) return;

			var msg = new SendGridMessage();

			msg.SetFrom(new EmailAddress("noreply@corewiki.jeffreyfritz.com", "No Reply Team"));

			var recipient = new EmailAddress(recipientEmail);

			msg.AddTo(recipient);
			msg.SetSubject(subject);

			// TODO: Permalink to comment
			msg.AddContent(MimeType.Html, body);

			var apiKey = _configuration["SendGridApiKey"];
			var client = new SendGridClient(apiKey);

			var response = await client.SendEmailAsync(msg);

		}

	}
}

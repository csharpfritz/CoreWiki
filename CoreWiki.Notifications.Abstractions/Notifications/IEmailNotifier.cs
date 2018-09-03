using System.Threading.Tasks;

namespace CoreWiki.Notifications.Abstractions.Notifications
{
	public interface IEmailNotifier
    {
		Task<bool> SendEmailAsync(string recipientEmail, string subject, string body);
		Task<bool> SendEmailAsync(string recipientEmail, string recipientName, string subject, string body);

	}
}
